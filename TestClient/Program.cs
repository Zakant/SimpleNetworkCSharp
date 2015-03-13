using SimpleNetwork.Client.Request;
using SimpleNetwork.Client.Secure;
using SimpleNetwork.Detection.Detector;
using SimpleNetwork.Package.Packages;
using SimpleNetwork.Package.Log;
using System;
using System.Linq;
using System.Threading;
using SimpleNetwork.Package.Packages.Request;
using SimpleNetwork.Package.Packages.Response;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            SecureClient client = new SecureClient();
            var log = client.createLog();

            client.RegisterPackageListener<TextPackage>((p, c) =>
                {
                    //Console.WriteLine(p.Value);
                });
            client.ConnectionStateChanged += ((s, e) =>
                {
                    Console.WriteLine("Conenction state {0} => {1}", e.OldState, e.NewState);
                });
            NetworkDetector d = new NetworkDetector();
            d.StartDetection();
            while (d.Hosts.Count() < 1) { Thread.Sleep(500); }


            client.Connect(d.Hosts.First());


            var request = client.createRequest<TextRequestPackage, TextResponsePackage>((new TextRequestPackage("Hallo Welt")));

            request.Send();
            request.WaitOne();
            var result = request.ResponsePackage;

            Console.ReadKey(false);
            for (int i = 0; i < 5; i++)
            {
                client.SendPackage(new TextPackage(String.Format("Test {0}", i)));
            }
            Console.WriteLine("Done sending");
            Console.ReadKey(false);
            d.StopDetection();
            client.Disconnect();
        }
    }
}
