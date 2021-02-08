using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RpcLibrary
{
    public class RpcServer
    {
        public int RpcPort { get; private set; }
        public string RpcIPAddress { get; private set; }

        private IPEndPoint rpcPoint;
        private Socket listenSocket;

        ~RpcServer()
        {
            listenSocket.Shutdown(SocketShutdown.Both);
            listenSocket.Close();
        }

        public void CreateDefaultAddress()
        {
            RpcIPAddress = "127.0.0.2"; // localhost by default
            RpcPort = 8006; // port by default
            rpcPoint = new IPEndPoint(IPAddress.Parse(RpcIPAddress), RpcPort);
        }

        public void CreateAddress(string address, int port)
        {
            IPAddress iPAddress;

            if (IPAddress.TryParse(address, out iPAddress) == false)
            {
                iPAddress = IPAddress.Parse("127.0.0.2");
                RpcIPAddress = "127.0.0.2";
                // log : Invalid IP Address. Default Address (127.0.0.2) is set.
            }
            else
            {
                RpcIPAddress = address;
            }

            try
            {
                RpcPort = port;
                rpcPoint = new IPEndPoint(iPAddress, RpcPort);
            }
            catch (ArgumentOutOfRangeException e)
            {
                RpcPort = 8006;
                rpcPoint = new IPEndPoint(iPAddress, RpcPort);
                // log : Error: Invalid Port. Default Port (8006) is set.
            }
        }

        public async void ListenAsync()
        {
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listenSocket.Bind(rpcPoint);
                listenSocket.Listen(10);

                await Task.Run(Listen);
            }
            catch (SocketException e)
            {
                // log : Error: Socket unavailable.
                // log : Error: Server cannot be started.
            }
            catch (Exception e)
            {
                // log : Error: Server cannot be started.
            }
        }

        private void Listen()
        {
            while (true)
            {
                // log : Waiting for Connection

                Socket handler = listenSocket.Accept();

                // log : Connection Accepted

                while (handler.Available == 0) { }

                // log : Receiving Data

                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                byte[] data = new byte[handler.Available];

                do
                {
                    bytes = handler.Receive(data);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (handler.Available > 0);

                // log : Data Received

                // parse request

                // execute method

                // parse response

                // log : Sending Responce

                string responce = "Server Responce";
                data = new byte[responce.Length];
                data = Encoding.Unicode.GetBytes(responce);
                handler.Send(data);

                // log : Responce send

                // log : Closing Connection

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
                
                // log : Connection Close
            }
        }
    }
}