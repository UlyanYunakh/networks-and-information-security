using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace RpcLibrary
{
    public class RpcClient
    {
        public int RpcServerPort { get; set; } = 8006; // server's port by default
        public string RpcServerIPAddress { get; set; } = "127.0.0.2"; // server's localhost by default

        public async Task<string> SendRequestAsync(string jsonRpcRequest)
        {
            return await Task.Run(() => SendRequest(jsonRpcRequest));
        }

        private string SendRequest(string jsonRpcRequest)
        {
            Socket sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            if (ConnectToServer(sendSocket) == false)
            {
                // log : Error: No connection with server
                return null;
            }

            // log : Successful connection

            // log : Sending request

            byte[] data = Encoding.Unicode.GetBytes(jsonRpcRequest);
            sendSocket.Send(data);

            // log : Request send

            // log : Waiting responce

            while (sendSocket.Available == 0) { }

            // log : Responce accepted

            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            data = new byte[sendSocket.Available];

            do
            {
                bytes = sendSocket.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (sendSocket.Available > 0);

            sendSocket.Shutdown(SocketShutdown.Both);
            sendSocket.Close();

            return builder.ToString();
        }

        private bool ConnectToServer(Socket sendSocket)
        {
            IPEndPoint rpcServerPoint = null;

            if (CreateAddress(ref rpcServerPoint) == false)
            {
                // log : Error: Cannot connect to server. Invalid Address.
                return false;
            }

            try
            {
                sendSocket.Connect(rpcServerPoint);
            }
            catch (SocketException e)
            {
                // log : Error: Socket unavailable.
                // log : Error: Cannot connect to server.
                return false;
            }
            catch (Exception e)
            {
                // log : Error: Cannot connect to server.
                return false;
            }
            return true;
        }

        private bool CreateAddress(ref IPEndPoint rpcServerPoint)
        {
            IPAddress iPAddress;

            if (IPAddress.TryParse(RpcServerIPAddress, out iPAddress) == false)
            {
                // log : Invalid IP.
                return false;
            }

            try
            {
                rpcServerPoint = new IPEndPoint(iPAddress, RpcServerPort);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // log : Error: Invalid Port.
                return false;
            }

            return true;
        }
    }
}