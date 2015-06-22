using SimpleNetwork.Detection.Announcer;
using SimpleNetwork.Package.Listener;
using SimpleNetwork.Package.Packages;
using SimpleNetwork.Server.Secure;
using SimpleNetwork.Server.Response;
using SimpleNetwork.Package.Log;
using System;
using System.Linq;
using SimpleNetwork.Package.Packages.Request;
using SimpleNetwork.Package.Packages.Response;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            DhSecureServer server = new DhSecureServer(8000);
            var log = server.createLog();

            server.EnableResponseSystem();
            server.Start();

            NetworkAnnouncer ann = new NetworkAnnouncer(server);

            ann.StartAnnouncing();

            server.RegisterPackageListener<TextRequestPackage>((p, c) =>
                {
                    p.SendResponse(new TextResponsePackage("Hallo Da!"));
                });

            server.RegisterPackageListener<TextPackage>(new LambdaPackageListener<TextPackage>((text, client) =>
                {
                    Console.WriteLine(text.Value);
                    client.SendPackage(new TextPackage("Heureka"));
                }));

            server.ClientDisconnected += new EventHandler<SimpleNetwork.Events.ClientDisconnectedEventArgs>((sender, a) =>
            {
                Console.WriteLine("Client disconnected: " + a.Reason.ToString());
            });

            Console.ReadKey(false);
            var test = log.Logs.First();
            ann.StopAnnouncing();
            server.Stop();
        }
    }
}
