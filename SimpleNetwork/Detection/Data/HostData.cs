using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SimpleNetwork.Detection.Data
{
    [Serializable]
    public class HostData 
    {
        public Guid ID { get; set; }

        public IPAddress[] Addresses { get; set; }

        public int Port { get; set; }

        public string Name { get; set; }


        public byte[] toByteArray()
        {
            BinaryFormatter f = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            f.Serialize(ms, this);
            return ms.GetBuffer();
        }

        public static HostData fromByteArray(byte[] data)
        {
            BinaryFormatter f = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(data);
            return f.Deserialize(ms) as HostData;
        }
    }
}
