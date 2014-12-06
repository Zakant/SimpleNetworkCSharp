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
using SimpleNetwork.Server.Secure;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SecureServer server = new SecureServer(8000);
            server.Start();

            NetworkAnnouncer ann = new NetworkAnnouncer(server);

            ann.StartAnnouncing();

            server.RegisterPackageListener<TextPackage>(new LambdaPackageListener<TextPackage>((text, client) =>
                {
                    Console.WriteLine(text.Value);
                    client.SendPackage(new TextPackage() { Value = "Heureka" });
                    client.SendPackage(new TextPackage("Heureka2"));
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
