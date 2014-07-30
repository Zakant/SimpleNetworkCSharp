using SimpleNetwork.Client;
using SimpleNetwork.Detection;
using SimpleNetwork.Detection.Detector;
using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            client.RegisterPackageListener<TextPackage>((p, c) =>
                {
                    Console.WriteLine(p.Value);
                });
            PushNetworkDetector d = new PushNetworkDetector();
            d.StartDetection();
            while (d.Hosts.Count() < 1) { }


            client.Connect(d.Hosts.First());
            client.SendPackage(new TextPackage("Hallo Welt"));

            Console.ReadKey();
            d.StopDetection();
            client.Disconnect();
        }
    }
}
