using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Client.Secure.Modules.StreamBased
{
    public interface IStreamBasedSecureModule : ISecureModule
    {

        Stream wrapInputStream(Stream inStream);
        Stream wrapOutputStream(Stream outStream);
    }
}
