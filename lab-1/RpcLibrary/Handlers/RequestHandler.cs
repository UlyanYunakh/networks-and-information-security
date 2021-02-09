using System;
using System.Reflection;
using System.Collections.Generic;
using RpcLibrary.Models;

namespace RpcLibrary.Handlers
{
    internal static class RequestHandler
    {
        internal static void Handle(string json)
        {
            if (JsonRpcParser.ValidateJson(json) == false)
            {
                // error -32700
                return;
            }

            Type paramsType = GetRequestParamsType(json);
            if (paramsType == null)
            {
                // error -32600
                return;
            }

            if (paramsType == typeof(object[]))
            {
                HandleRequest(json);
            }
            else if (paramsType == typeof(Dictionary<string, object>))
            {
                HandleNamedRequest(json);
            }

        }
        private static Type GetRequestParamsType(string json)
        {
            Type type;
            if (JsonRpcParser.TryParseRequest<object[]>(json, out type) == true)
            {
                return type;
            }
            else if (JsonRpcParser.TryParseRequest<Dictionary<string, object>>(json, out type) == true)
            {
                return type;
            }
            else
            {
                return null;
            }
        }

        private static void HandleRequest(string json)
        {
            JsonRpcRequest<object[]> request = JsonRpcParser.ParseRequest<object[]>(json);

            MethodInfo method = null;

            if (ValidateMethod(request.method, out method) == false && method == null)
            {
                // error -32601
                return;
            }

            object result = ExecuteMethod(method, request.@params);
        }

        private static void HandleNamedRequest(string json)
        {
            JsonRpcRequest<Dictionary<string, object>> request = JsonRpcParser.ParseRequest<Dictionary<string, object>>(json);

            MethodInfo method = null;

            if (ValidateMethod(request.method, out method) == false && method == null)
            {
                // error -32601
                return;
            }

            object result = ExecuteNamedMethod(method, request.@params);
        }

        private static bool ValidateMethod(string methodName, out MethodInfo method)
        {
            try
            {
                method = RpcServer.Procedures.GetType().GetMethod(methodName);
            }
            catch
            {
                method = null;
                return false;
            }

            return true;
        }

        private static object ExecuteNamedMethod(MethodInfo method, Dictionary<string, object> @params)
        {
            if (method.GetParameters().Length != @params.Count)
            {
                // error -32602
                return null;
            }

            object[] objParams = new object[method.GetParameters().Length];

            int count = 0;
            foreach (ParameterInfo param in method.GetParameters())
            {
                int i = count;
                foreach (KeyValuePair<string, object> keyValue in @params)
                {
                    if (keyValue.Key == param.Name)
                    {
                        objParams[count] = keyValue.Value;
                        count++;
                        break;
                    }
                }
                if (i == count)
                {
                    // error -32602
                    return null;
                }
            }

            object result = ExecuteMethod(method, objParams);
            return result;
        }

        private static object ExecuteMethod(MethodInfo method, object[] @params)
        {
            object result = null;

            try
            {
                result = method.Invoke(RpcServer.Procedures, @params);
            }
            catch (ArgumentException e)
            {
                // error -32602
                return null;
            }
            catch (TargetParameterCountException e)
            {
                // error -32602
                return null;
            }
            catch
            {
                // error -32603
                return null;
            }

            return result;
        }
    }
}