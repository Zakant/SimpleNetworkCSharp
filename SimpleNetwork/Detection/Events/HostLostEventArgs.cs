using SimpleNetwork.Detection.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Detection.Events
{
    [Serializable]
    public class HostLostEventArgs : EventArgs
    {
        public HostData OldData { get; protected set; }

        public HostLostEventArgs(HostData oldData)
        {
            this.OldData = oldData;
        }
    }
}
