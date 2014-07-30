using SimpleNetwork.Package.Packages;
using SimpleNetwork.Package.Listener;
using SimpleNetwork.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleNetwork.Detection;
using SimpleNetwork.Detection.Announcer;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(8000);
            server.Start();

            PushNetworkAnnouncer ann = new PushNetworkAnnouncer(server);

            ann.StartAnnouncing();

            server.RegisterPackageListener<TextPackage>(new LambdaPackageListener<TextPackage>((text, client) =>
                {
                    Console.WriteLine(text.Value);
                    client.SendPackage(new TextPackage() { Value = "Heureka" });
                }));

            server.ClientDisconnected += new EventHandler<SimpleNetwork.Events.ClientDisconnectedEventArgs>((sender, a) =>
            {
                Console.WriteLine("Client disconnected: " + a.Reason.ToString());
            });

            Console.ReadKey();
            ann.StopAnnouncing();
            server.Stop();
        }
    }
}
