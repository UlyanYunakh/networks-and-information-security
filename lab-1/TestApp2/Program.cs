using System;
using System.Threading;
using System.Text;
using Newtonsoft.Json;
using RpcLibrary;
using RpcLibrary.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            RpcServer server = new RpcServer()
            {
                RpcIPAddress = "127.0.0.6",
                RpcPort = 8007
            };
            RpcServer.Procedures = new Procedures();
            server.LogNotify += DisplayMessage;
            server.ListenAsync();

            RpcClient client = new RpcClient()
            {
                RpcServerIPAddress = "127.0.0.2",
                RpcServerPort = 8006
            };
            client.LogNotify += DisplayMessage;

            var r1 = await client.SendRequestAsync(@"{'jsonrpc': '2.0', 'method': 'Sum', 'params': [2,8], 'id': 4}");
            var r2 = await client.SendRequestAsync(@"{'jsonrpc': '2.0', 'method': 'Print', 'id': }");
            
            Console.WriteLine(r1);
            Console.WriteLine(r2);

            Console.ReadKey();
        }

        private static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
