using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Blueprint41.Driver
{
    public class TrustManager
    {


        public static TrustManager CreateInsecure(bool verifyHostname = false) => null!;
        public static TrustManager CreateChainTrust(bool verifyHostname = true, X509RevocationMode revocationMode = X509RevocationMode.NoCheck, X509RevocationFlag revocationFlag = X509RevocationFlag.ExcludeRoot, bool useMachineContext = false) => null!;
        public static TrustManager CreatePeerTrust(bool verifyHostname = true, bool useMachineContext = false) => null!;
        public static TrustManager CreateCertTrust(IEnumerable<X509Certificate2> trusted, bool verifyHostname = true) => null!;
    }
}
