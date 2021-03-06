﻿using SimpleNetwork.Client;
using SimpleNetwork.Events;
using SimpleNetwork.Package.Listener;
using SimpleNetwork.Package.Packages;
using System;

namespace SimpleNetwork.Package.Provider
{
    /// <summary>
    /// Stellt Methoden bereit, um Packete an <see cref="SimpleNetwork.Package.Listener.IPackageListener" /> weiterzuleiten.
    /// </summary>
    public interface IPackageProvider
    {
        /// <summary>
        /// Tritt ein, wenn eine neue Nachricht eintrifft.
        /// </summary>
        event EventHandler<MessageInEventArgs> MessageIn;
        /// <summary>
        /// Tritt ein, wenn eine Nachricht gesendet werden soll.
        /// </summary>
        event EventHandler<MessageOutEventArgs> MessageOut;
        /// <summary>
        /// Registriert einen neuen <see cref="SimpleNetwork.Package.Listener.IPackageListener" />.
        /// </summary>
        /// <typeparam name="T">Den Packettyp, den der <see cref="SimpleNetwork.Package.Listener.IPackageListener" /> abbonieren möchte.</typeparam>
        /// <param name="packagelistener">Der <see cref="SimpleNetwork.Package.Listener.IPackageListener" />.</param>
        void RegisterPackageListener<T>(IPackageListener packagelistener) where T : IPackage;

        /// <summary>
        /// Reigstriert einen neunen Listener. Dieser wird durch eine <see cref="Action{T,T2}" /> dargestellt.
        /// </summary>
        /// <typeparam name="T">Der Typ, auf den gelauscht wird.</typeparam>
        /// <param name="action">Die <see cref="Action{T,T2}" />.</param>
        void RegisterPackageListener<T>(Action<T, IClient> action) where T : IPackage;

        /// <summary>
        /// Entfernt einen <see cref="SimpleNetwork.Package.Listener.IPackageListener" />.
        /// </summary>
        /// <typeparam name="T">Der Packettype, den <see cref="SimpleNetwork.Package.Listener.IPackageListener" /> abboniert hat.</typeparam>
        /// <param name="packagelistener">Der <see cref="SimpleNetwork.Package.Listener.IPackageListener" />, der entfernt werden soll.</param>
        void RemovePackageListener<T>(IPackageListener packagelistener) where T : IPackage;

        /// <summary>
        /// Entfernt alle Listener eines bestimmten Types.
        /// </summary>
        /// <typeparam name="T">Der Typ, von dem alle Listener entfernt werden sollen.</typeparam>
        void RemoveTypeListener<T>() where T : IPackage;
    }
}
