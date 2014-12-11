using SimpleNetwork.Events.Secure;
using System;

namespace SimpleNetwork.Client.Secure
{
    public interface ISecureClient : IClient
    {
        event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;

        byte[] PublicKey { get; }
        byte[] SharedKey { get; }
        ConnectionState State { get; }
    }
}
