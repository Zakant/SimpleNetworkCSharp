using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SimpleNetwork.Detection.Data
{
    /// <summary>
    /// Repräsentiert die Verbindungsinformationen eines Hosts.
    /// </summary>
    [Serializable]
    public class HostData 
    {
        /// <summary>
        /// Die ID des Hots.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Eine Auflistung aller möglichen IP Adressen des Hosts.
        /// </summary>
        public IPAddress[] Addresses { get; set; }

        /// <summary>
        /// Der Port des Hosts.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Der Name des Hosts.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Eine zusätzliche Beschreibung des Hosts.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Weitere zusätzliche Informationen über den Host. Nicht zur direkten Anzeige gedacht.
        /// </summary>
        public string AdditionalInformation { get; set; }

        /// <summary>
        /// Konvertiert diese HostData-Objekt in seine Byte Array Darstellung.
        /// </summary>
        /// <returns>Die Byte Array Darstellung diese HostData-Objektes</returns>
        public byte[] toByteArray()
        {
            BinaryFormatter f = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            f.Serialize(ms, this);
            return ms.GetBuffer();
        }

        /// <summary>
        /// Konvertiert eine Bytefolge in ein HostData-Objekt.
        /// </summary>
        /// <param name="data">Die Bytefolge, aus der das HostData-Objekt erzeugt wird.</param>
        /// <returns>Das erzeugte HostData-Objekt.</returns>
        public static HostData fromByteArray(byte[] data)
        {
            BinaryFormatter f = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(data);
            return f.Deserialize(ms) as HostData;
        }
    }
}
