using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleNetwork.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
