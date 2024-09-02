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

        public static TrustManager CreateInsecure(bool verifyHostname = false) => null!;
        public static TrustManager CreateChainTrust(bool verifyHostname = true, X509RevocationMode revocationMode = X509RevocationMode.NoCheck, X509RevocationFlag revocationFlag = X509RevocationFlag.ExcludeRoot, bool useMachineContext = false) => null!;
        public static TrustManager CreatePeerTrust(bool verifyHostname = true, bool useMachineContext = false) => null!;
        public static TrustManager CreateCertTrust(IEnumerable<X509Certificate2> trusted, bool verifyHostname = true) => null!;





        //public static TrustManager CreateInsecure(bool verifyHostname) => null!;
        //public static TrustManager CreateChainTrust(bool verifyHostname, X509RevocationMode revocationMode, X509RevocationFlag revocationFlag, bool useMachineContext) => null!;
        //public static TrustManager CreatePeerTrust(bool verifyHostname, bool useMachineContext) => null!;
        //public static TrustManager CreateCertTrust(IEnumerable<X509Certificate2> trusted, bool verifyHostname) => null!;
    }
}
