using SimpleNetwork.Events.Secure;
using SimpleNetwork.Package.Packages;
using SimpleNetwork.Package.Packages.Internal.Secure;
using SimpleNetwork.Package.Packages.Secure;
using SimpleNetwork.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace SimpleNetwork.Client.Secure
{
    public class SecureClient : Client, ISecureClient
    {
        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        protected ConnectionStateChangedEventArgs RaiseConnectionStateChanged(ConnectionState Old, ConnectionState New)
        {
            var myevent = ConnectionStateChanged;
            var args = new ConnectionStateChangedEventArgs(Old, New);
            if (myevent != null)
                myevent(this, args);
            return args;
        }


        public byte[] PublicKey { get; protected set; }
        public byte[] SharedKey { get; protected set; }

        private ConnectionState _state = ConnectionState.Undefined;
        public ConnectionState State
        {
            get { return _state; }
            set
            {
                var old = _state;
                _state = value;
                RaiseConnectionStateChanged(old, State);
            }
        }

        protected Queue<IPackage> outqueue = new Queue<IPackage>();
        protected Queue<CryptoPackage> inqueue = new Queue<CryptoPackage>();

        private ECDiffieHellmanCng dh;
        private RijndaelManaged rjd;

        private ICryptoTransform EncTransform;
        private ICryptoTransform DecTransform;

        public SecureClient()
            : base()
        {
            Init();
        }

        public SecureClient(TcpClient client)
            : base(client)
        {
            Init();
        }

        protected void Init()
        {
            State = ConnectionState.Unsecure;
            ConnectionStateChanged += (s, e) =>
            {
                if (e.NewState == ConnectionState.Secure)
                {
                    foreach (var p in outqueue)
                        SendPackage(p);
                    foreach (var p in inqueue)
                        HandleNewMessage(p);
                }
            };
        }

        protected void InitializeSecureConnection()
        {
            dh = new ECDiffieHellmanCng();
            rjd = new RijndaelManaged();
            PublicKey = dh.PublicKey.ToByteArray();
            if (isServerClient)
                SendUnsecure(new ServerSecurePackage(PublicKey, rjd.IV));
        }

        protected override void OpenConnection()
        {
            RemoveTypeListener<ClientSecurePackage>();
            RemoveTypeListener<ServerSecurePackage>();
            // Listener hinzufügen
            if (isServerClient)
                RegisterPackageListener<ClientSecurePackage>((p, c) =>
                {
                    MakeSecure(p.PublicKey, rjd.IV);
                });
            else
                RegisterPackageListener<ServerSecurePackage>((p, c) =>
                    {
                        SendUnsecure(new ClientSecurePackage(PublicKey));
                        MakeSecure(p.PublicKey, p.IV);
                    });

            base.OpenConnection();
            InitializeSecureConnection();
        }

        protected void MakeSecure(byte[] PublicKey, byte[] iv)
        {
            State = ConnectionState.Securing;
            SharedKey = dh.DeriveKeyMaterial(CngKey.Import(PublicKey, CngKeyBlobFormat.EccPublicBlob));
            rjd.Key = SharedKey;
            rjd.IV = iv;
            DecTransform = rjd.CreateDecryptor();
            EncTransform = rjd.CreateEncryptor();
            State = ConnectionState.Secure;
        }


        protected void SendUnsecure(IPackage package)
        {
            base.SendPackage(package);
        }

        public override void SendPackage(IPackage package)
        {
            if (State != ConnectionState.Secure)
                outqueue.Enqueue(package);
            else
                base.SendPackage(new CryptoPackage(EncryptPackage(package)));
        }

        public IPackage decryptPackage(CryptoPackage cryptoPackage)
        {
            if (cryptoPackage == null) throw new ArgumentNullException("cryptoPackage");
            return DecryptPackage(cryptoPackage.Data);
        }

        protected override void HandleNewMessage(IPackage p)
        {
            if (p is CryptoPackage)
            {
                if (State != ConnectionState.Secure)
                    inqueue.Enqueue(p as CryptoPackage);
                else
                    base.HandleNewMessage(DecryptPackage((p as CryptoPackage).Data));
            }
            else
                base.HandleNewMessage(p);
        }

        protected byte[] EncryptPackage(IPackage p)
        {
            if (p == null) throw new ArgumentNullException("p");
            byte[] data = p.ToByteArray();
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, EncTransform, CryptoStreamMode.Write))
                    cs.Write(data, 0, data.Length);
                return ms.ToArray();
            }

        }

        protected IPackage DecryptPackage(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, DecTransform, CryptoStreamMode.Write))
                    cs.Write(data, 0, data.Length);
                return ms.ToArray().ToPackage();
            }
        }
    }
}
