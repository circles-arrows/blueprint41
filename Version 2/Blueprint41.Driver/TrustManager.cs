using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Blueprint41.Driver
{
    public class TrustManager
    {
        internal TrustManager(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public static TrustManager CreateInsecure(bool verifyHostname = false) => Driver.TRUST_MANAGER.CreateInsecure(verifyHostname);
        public static TrustManager CreateChainTrust(bool verifyHostname = true, X509RevocationMode revocationMode = X509RevocationMode.NoCheck, X509RevocationFlag revocationFlag = X509RevocationFlag.ExcludeRoot, bool useMachineContext = false) => Driver.TRUST_MANAGER.CreateChainTrust(verifyHostname);
        public static TrustManager CreatePeerTrust(bool verifyHostname = true, bool useMachineContext = false) => Driver.TRUST_MANAGER.CreatePeerTrust(verifyHostname, useMachineContext);
        public static TrustManager CreateCertTrust(IEnumerable<X509Certificate2> trusted, bool verifyHostname = true) => Driver.TRUST_MANAGER.CreateCertTrust(trusted, verifyHostname);

        public override bool Equals(object obj) => Driver.TRUST_MANAGER.Equals(_instance, obj);
        public override int GetHashCode() => Driver.TRUST_MANAGER.GetHashCode(_instance);
    }
}
