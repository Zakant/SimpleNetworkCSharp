using SimpleNetwork.Client;
using SimpleNetwork.Events;
using SimpleNetwork.Package.Listener;
using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Provider
{
    /// <summary>
    /// Stellt Methoden bereit, um Packete an <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" /> weiterzuleiten.
    /// </summary>
    public interface IPackageProvider
    {
        /// <summary>
        /// Tritt ein, wenn eine neue Nachricht eintrifft.
        /// </summary>
        event EventHandler<NewMessageEventArgs> NewMessage;

        /// <summary>
        /// Registriert einen neuen <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />.
        /// </summary>
        /// <typeparam name="T">Den Packettyp, den der <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" /> abbonieren möchte.</typeparam>
        /// <param name="packagelistener">Der <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />.</param>
        void RegisterPackageListener<T>(IPackageListener<T> packagelistener) where T : IPackage;

        /// <summary>
        /// Reigstriert einen neunen Listener. Dieser wird durch eine <see cref="Action{T,T2}" /> dargestellt.
        /// </summary>
        /// <typeparam name="T">Der Typ, auf den gelauscht wird.</typeparam>
        /// <param name="action">Die <see cref="Action{T,T2}" />.</param>
        void RegisterPackageListener<T>(Action<T, IClient> action) where T : IPackage;

        /// <summary>
        /// Entfernt einen <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />.
        /// </summary>
        /// <typeparam name="T">Der Packettype, den <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" /> abboniert hat.</typeparam>
        /// <param name="packagelistener">Der <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />, der entfernt werden soll.</param>
        void RemovePackageListener<T>(IPackageListener<T> packagelistener) where T : IPackage;

        /// <summary>
        /// Entfernt alle Listener eines bestimmten Types.
        /// </summary>
        /// <typeparam name="T">Der Typ, von dem alle Listener entfernt werden sollen.</typeparam>
        void RemoveTypeListener<T>() where T : IPackage;
    }
}
