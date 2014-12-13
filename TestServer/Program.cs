using SimpleNetwork.Detection.Announcer;
using SimpleNetwork.Package.Listener;
using SimpleNetwork.Package.Packages;
using SimpleNetwork.Server.Secure;
using SimpleNetwork.Package.Log;
using System;
using System.Linq;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SecureServer server = new SecureServer(8000);
            var log = server.createLog();

            server.Start();

            NetworkAnnouncer ann = new NetworkAnnouncer(server);

            ann.StartAnnouncing();

            server.RegisterPackageListener<TextPackage>(new LambdaPackageListener<TextPackage>((text, client) =>
                {
                    Console.WriteLine(text.Value);
                    client.SendPackage(new TextPackage("Heureka"));
                }));

            server.ClientDisconnected += new EventHandler<SimpleNetwork.Events.ClientDisconnectedEventArgs>((sender, a) =>
            {
                Console.WriteLine("Client disconnected: " + a.Reason.ToString());
            });

            Console.ReadKey();
            var test = log.Logs.First();
            ann.StopAnnouncing();
            server.Stop();
        }
    }
}
