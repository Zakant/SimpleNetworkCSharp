#if __MonoCS__

using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;



namespace System.Security.Cryptography
{

    internal sealed class ECDiffieHellmanCng : ECDiffieHellman
    {
        public ECDiffieHellmanCng();

        [SecurityCritical]
        public ECDiffieHellmanCng(CngKey key);

        [SecurityCritical]
        public ECDiffieHellmanCng(int keySize);

        public CngAlgorithm HashAlgorithm { get; set; }

        public byte[] HmacKey { get; set; }

        public CngKey Key { get; }

        public ECDiffieHellmanKeyDerivationFunction KeyDerivationFunction { get; set; }

        public byte[] Label { get; set; }

        public override ECDiffieHellmanPublicKey PublicKey { get; }

        public byte[] SecretAppend { get; set; }

        public byte[] SecretPrepend { get; set; }

        public bool UseSecretAgreementAsHmacKey { get; }

        [SecurityCritical]
        public byte[] DeriveKeyMaterial(CngKey otherPartyPublicKey);

        public override byte[] DeriveKeyMaterial(ECDiffieHellmanPublicKey otherPartyPublicKey);

        [SecurityCritical]
        public SafeNCryptSecretHandle DeriveSecretAgreementHandle(CngKey otherPartyPublicKey);

        public SafeNCryptSecretHandle DeriveSecretAgreementHandle(ECDiffieHellmanPublicKey otherPartyPublicKey);

        protected override void Dispose(bool disposing);

        public override void FromXmlString(string xmlString);

        public void FromXmlString(string xml, ECKeyXmlFormat format);

        public override string ToXmlString(bool includePrivateParameters);

        public string ToXmlString(ECKeyXmlFormat format);
    }
}

namespace Microsoft.Win32.SafeHandles
{
    [SecurityCritical(SecurityCriticalScope.Everything)]
    public sealed class SafeNCryptSecretHandle : SafeNCryptHandle
    {
        public SafeNCryptSecretHandle();

        protected override bool ReleaseNativeHandle();
    }
}

#endif
