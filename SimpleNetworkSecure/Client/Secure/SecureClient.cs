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
    /// <summary>
    /// Stellt eine Klasse zum Aufnehmen einer sicheren Verbindung da.
    /// </summary>
    public class SecureClient : Client, ISecureClient
    {
        /// <summary>
        /// Tritt ein, wenn sich der Zustand der sicheren Verbindung veränert.
        /// </summary>
        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;

        /// <summary>
        /// Löst das <see cref="SimpleNetwork.Client.Secure.SecureClient.ConnectionStateChanged"/> Ereignis aus.
        /// </summary>
        /// <param name="Old">Der alte Zustand der Verbindung.</param>
        /// <param name="New">Der neue Zustand der Verbindung.</param>
        /// <returns>Die verwendeten Ereignisargumente.</returns>
        protected ConnectionStateChangedEventArgs RaiseConnectionStateChanged(ConnectionState Old, ConnectionState New)
        {
            var myevent = ConnectionStateChanged;
            var args = new ConnectionStateChangedEventArgs(Old, New);
            if (myevent != null)
                myevent(this, args);
            return args;
        }

        /// <summary>
        /// Der öffentliche Schlüssel der Verbindung.
        /// </summary>
        public byte[] PublicKey { get; protected set; }

        /// <summary>
        /// Der geheime Schlüssel der Verbindung
        /// </summary>
        public byte[] SharedKey { get; protected set; }

        private ConnectionState _state = ConnectionState.Undefined;

        /// <summary>
        /// Der aktuelle Zustand der Verbindung.
        /// </summary>
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

        /// <summary>
        /// Eine Sammlung von Paketen die aufgrund des Zustandes der Verbindung noch nicht gesendet wurden.
        /// </summary>
        protected Queue<IPackage> outqueue = new Queue<IPackage>();

        /// <summary>
        /// Eine Sammlung von Paketen die aufgrund des Zustandes der Verbindung noch nicht entschlüsselt wurden.
        /// </summary>
        protected Queue<CryptoPackage> inqueue = new Queue<CryptoPackage>();

        private ECDiffieHellmanCng dh;
        private RijndaelManaged rjd;

        private ICryptoTransform EncTransform;
        private ICryptoTransform DecTransform;

        /// <summary>
        /// Erzeugt eine neue Verbindungsklasse.
        /// </summary>
        public SecureClient()
            : base()
        {
            Init();
        }

        /// <summary>
        /// Erzeugt einen neue Verbindungsklasse aufgrund eines TcpClients.
        /// </summary>
        /// <param name="client"></param>
        public SecureClient(TcpClient client)
            : base(client)
        {
            Init();
        }

        /// <summary>
        /// Initialisiert die Verbindung.
        /// </summary>
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

        /// <summary>
        /// Initialisiert die Dicherung der Verbindung.
        /// </summary>
        protected void InitializeSecureConnection()
        {
            dh = new ECDiffieHellmanCng();
            rjd = new RijndaelManaged();
            PublicKey = dh.PublicKey.ToByteArray();
            if (isServerClient)
                SendUnsecure(new ServerSecurePackage(PublicKey, rjd.IV));
        }

        /// <summary>
        /// Wird aufgerufen wenn der Client-Server Handschlag abgeschlossen wurde.
        /// </summary>
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

        /// <summary>
        /// Sichert die Verbindung krypthographisch ab.
        /// </summary>
        /// <param name="PublicKey">Der zu verwendene Schlüssel.</param>
        /// <param name="iv">Der zu verwendene Initialisierungsvektor.</param>
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

        /// <summary>
        /// Sendet ein Paket auch über eine ungesicherte Verbindung.
        /// </summary>
        /// <param name="package"></param>
        protected void SendUnsecure(IPackage package)
        {
            base.SendPackage(package);
        }

        /// <summary>
        /// Sendet ein Paket an den Remotehost.
        /// </summary>
        /// <param name="package">Das zu sendene Paket.</param>
        public override void SendPackage(IPackage package)
        {
            if (State != ConnectionState.Secure)
                outqueue.Enqueue(package);
            else
                base.SendPackage(package);
        }

        /// <summary>
        /// Entschlüsselt ein Paket.
        /// </summary>
        /// <param name="cryptoPackage">Das zu entschlüsselnde Paket.</param>
        /// <returns>Das entschlüsselte Paket.</returns>
        public IPackage decryptPackage(CryptoPackage cryptoPackage)
        {
            if (cryptoPackage == null) throw new ArgumentNullException("cryptoPackage");
            return DecryptPackage(cryptoPackage.Data);
        }

        /// <summary>
        /// Behandelt ein neu eigegangenes Paket.
        /// </summary>
        /// <param name="p">Das zu behandelnde Paket.</param>
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

        /// <summary>
        /// Transformiert ein Paket so, dass es an den Remotehost gesendet werden kann.
        /// </summary>
        /// <param name="package">Das urspruengliche Paket.</param>
        /// <returns>Das transformierte Paket.</returns>
        protected override IPackage transformPackageForSend(IPackage package)
        {
            if (State == ConnectionState.Secure)
                return new CryptoPackage(EncryptPackage(package));
            return package;
        }

        /// <summary>
        /// Entschlüsselt ein Paket.
        /// </summary>
        /// <param name="p">Das zu entschlüsselnde Paket.</param>
        /// <returns>Das entschlüsselte Paket.</returns>
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

        /// <summary>
        /// Entschlüssel ein Paket aus einem Byte-Array.
        /// </summary>
        /// <param name="data">Das Byte-Array aus dem das Paket entschlüsselt wird.</param>
        /// <returns>Das entschlüsselte Paket.</returns>
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
