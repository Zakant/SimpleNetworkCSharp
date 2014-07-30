using SimpleNetwork.Detection.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Detection.Events
{
    [Serializable]
    public class HostFoundEventArgs : EventArgs
    {
        public HostData NewData { get; protected set; }

        public HostFoundEventArgs(HostData newData)
        {
            NewData = newData;
        }

    }
}
