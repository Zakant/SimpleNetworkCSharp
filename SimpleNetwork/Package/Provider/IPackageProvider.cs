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
        /// <typeparam name="T">Den Packettype, den der <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" /> abbonieren möchte.</typeparam>
        /// <param name="packagelistener">Der <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />.</param>
        void RegisterPackageListener<T>(IPackageListener<T> packagelistener) where T : IPackage;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void RegisterPackageListener<T>(Action<T, IClient> action) where T : IPackage;

        /// <summary>
        /// Entfernt einen <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />.
        /// </summary>
        /// <typeparam name="T">Der Packettype, den <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" /> abboniert hat.</typeparam>
        /// <param name="packagelistener">Der <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />, der entfernt werden soll.</param>
        void RemovePackageListener<T>(IPackageListener<T> packagelistener) where T : IPackage;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void RemoveTypeListener<T>() where T : IPackage;
    }
}
