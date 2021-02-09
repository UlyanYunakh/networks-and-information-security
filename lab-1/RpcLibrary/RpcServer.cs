using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using RpcLibrary.Handlers;

namespace RpcLibrary
{
    public class RpcServer
    {
        public int RpcPort { get; set; } = 8006; // port by default
        public string RpcIPAddress { get; set; } = "127.0.0.2"; // localhost by default

        public static object Procedures { get; set; } = null;

        private Socket listenSocket;
        private int connectionId = 0;

        ~RpcServer()
        {
            listenSocket.Shutdown(SocketShutdown.Both);
            listenSocket.Close();
        }

        public async void ListenAsync()
        {
            // log : Server start.

            if (Procedures == null)
            {
                // log : Server error: Server cannot be started. Procedures not set.
                return;
            }

            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint rpcPoint = null;

            if (CreateAddress(ref rpcPoint) == false)
            {
                // log : Server error: Server cannot be started. Invalid Address.
                return;
            }

            try
            {
                listenSocket.Bind(rpcPoint);
                listenSocket.Listen(10);
            }
            catch (SocketException e)
            {
                // log : Server error: Server cannot be started. Socket unavailable.
                return;
            }
            catch (Exception e)
            {
                // log : Server error: Server cannot be started.
                return;
            }

            await Task.Run(() => Listen());
        }

        private bool CreateAddress(ref IPEndPoint rpcPoint)
        {
            IPAddress iPAddress;

            if (IPAddress.TryParse(RpcIPAddress, out iPAddress) == false)
            {
                // log : Server error: Server address cannot be created. Invalid IP.
                return false;
            }

            try
            {
                rpcPoint = new IPEndPoint(iPAddress, RpcPort);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // log : Server error: Server address cannot be created. Invalid Port.
                return false;
            }

            return true;
        }

        private void Listen()
        {
            // log : Server: Server running

            while (true)
            {
                // log : Server: Waiting for connection

                Socket connectedSocket = listenSocket.Accept();

                // log : Server: Connection accepted

                Task.Run(() => ConnectionHandler(connectedSocket, connectionId++));
            }
        }

        private void ConnectionHandler(Socket connectedSocket, int id)
        {
            // log : Server: Connection {id}: Waiting request

            while (connectedSocket.Available == 0) { }

            // log : Server: Connection {id}: Receiving request

            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            byte[] data = new byte[connectedSocket.Available];

            do
            {
                bytes = connectedSocket.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (connectedSocket.Available > 0);

            // log : Server: Connection {id}: Request received

            bool isNotification = false;
            string responce = RequestHandler.Handle(builder.ToString(), out isNotification);


            if (!isNotification)
            {
                // log : Server: Connection {id}: Sending responce

                data = new byte[responce.Length];
                data = Encoding.Unicode.GetBytes(responce);
                connectedSocket.Send(data);

                // log : Server: Connection {id}: Responce sent
            }
            else
            {
                responce = " ";

                data = new byte[responce.Length];
                data = Encoding.Unicode.GetBytes(responce);
                connectedSocket.Send(data);

                // log : Notification accepted
            }

            // log : Server: Connection {id}: Closing connection

            connectedSocket.Shutdown(SocketShutdown.Both);
            connectedSocket.Close();

            // log : Server: Connection {id}: Connection closed
        }
    }
}