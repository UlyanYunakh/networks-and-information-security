using System;
using RpcLibrary;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            RpcServer server = new RpcServer();
            RpcServer.Procedures = new Procedures();
            server.LogNotify += DisplayMessage;
            server.ListenAsync();

            RpcClient client = new RpcClient()
            {
                RpcServerIPAddress = server.RpcIPAddress,
                RpcServerPort = server.RpcPort
            };
            client.LogNotify += DisplayMessage;

            var r1 = await client.SendRequestAsync(@"{'jsonrpc': '2.0', 'method': 'Sum', 'params': [2.3,7.8], 'id': 1}");
            var r2 = await client.SendRequestAsync(@"{'jsonrpc': '2.0', 'method': 'Print', 'id': 2}");
            var r3 = await client.SendRequestAsync(@"{'jsonrpc': '2thod': 'Print', 'id': 2}");

            Console.WriteLine(r1);
            Console.WriteLine(r2);
            Console.WriteLine(r3);

            Console.ReadKey();

            client = new RpcClient()
            {
                RpcServerIPAddress = "127.0.0.6",
                RpcServerPort = 8007
            };

            var r4 = await client.SendRequestAsync(@"{'jsonrpc': '2.0', 'method': 'Multi', 'params': [2.3,7.8], 'id': 1}");
            var r5 = await client.SendRequestAsync(@"{'jsonrpc': '2.0', 'method': 'Dot', 'id': 2}");
            
            Console.WriteLine(r4);
            Console.WriteLine(r5);

            Console.ReadKey();
        }

        private static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
