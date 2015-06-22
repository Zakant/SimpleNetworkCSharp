using SimpleNetwork.Package.Packages.Request;
using SimpleNetwork.Package.Packages.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Client.Request
{
    /// <summary>
    /// Stellt einen Vertrag für eine Anfrag da.
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// Der Typ des Anfrage Paketes.
        /// </summary>
        Type RequestType { get; }

        /// <summary>
        /// Der Typ des Antwort Paketes.
        /// </summary>
        Type ResponseType { get; }

        /// <summary>
        /// Der Client über den die Anfrage verschickt wird.
        /// </summary>
        IClient Client { get; }

        /// <summary>
        /// Gibt an, ob die Anfrage bereits versendet wurde.
        /// </summary>
        bool isSend { get; }

        /// <summary>
        /// Gibt an, ob die Anfrage sowieo Antwort bereits abgeschlossen wurden.
        /// </summary>
        bool isCompleted { get; }

        /// <summary>
        /// Sendet die Anfrage.
        /// </summary>
        void Send();

        /// <summary>
        /// Wartet auf das Empfangen einer Antwort.
        /// </summary>
        void WaitOne();

        /// <summary>
        /// Teilt der Anfrage die zugehörige Antwort mit.
        /// </summary>
        /// <param name="response">Die Antwort zu der Anfrage.</param>
        void ReceiveResponse(ResponsePackage response);
    }
}
