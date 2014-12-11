using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleNetwork.Client;
using System.IO;

namespace SimpleNetworkTest
{
    [TestClass]
    public class ClientTest
    {
        private Client c;
        private Stream In;
        private Stream Out;


        [TestInitialize]
        public void Init()
        {
            c = new Client();
            
        }


    }
}
