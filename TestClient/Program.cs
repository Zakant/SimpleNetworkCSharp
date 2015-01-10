using SimpleNetwork.Client.Secure;
using SimpleNetwork.Detection.Detector;
using SimpleNetwork.Package.Packages;
using SimpleNetwork.Package.Log;
using System;
using System.Linq;
using System.Threading;

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
            client.SendPackage(new TextPackage("Hallo Welt"));

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
