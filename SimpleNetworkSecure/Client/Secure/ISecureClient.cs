using SimpleNetwork.Events.Secure;
using SimpleNetwork.Package.Packages;
using SimpleNetwork.Package.Packages.Secure;
using System;

namespace SimpleNetwork.Client.Secure
{
    public interface ISecureClient : IClient
    {
        event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;

        byte[] PublicKey { get; }
        byte[] SharedKey { get; }
        ConnectionState State { get; }

        IPackage decryptPackage(CryptoPackage cryptoPackage);
    }
}
