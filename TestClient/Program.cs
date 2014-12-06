using SimpleNetwork.Client;
using SimpleNetwork.Client.Secure;
using SimpleNetwork.Detection;
using SimpleNetwork.Detection.Detector;
using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            SecureClient client = new SecureClient();
            client.RegisterPackageListener<TextPackage>((p, c) =>
                {
                    Console.WriteLine(p.Value);
                    Console.WriteLine("Key is: {0}", BitConverter.ToString(client.SharedKey));
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

            Console.ReadKey();
            client.SendPackage(new TextPackage("Test 2"));
            client.SendPackage(new TextPackage("Test 3"));
            client.SendPackage(new TextPackage("Test 4"));
            Console.ReadKey();
            d.StopDetection();
            client.Disconnect();
        }
    }
}
