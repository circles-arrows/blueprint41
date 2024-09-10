using System;
using System.Collections.Generic;
using System.Text;

using Blueprint41.Persistence;

namespace Blueprint41
{
    public sealed class AuthToken
    {
        internal AuthToken(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public static AuthToken None => Driver.AUTH_TOKENS.None;
        public static AuthToken Basic(string username, string password) => Driver.AUTH_TOKENS.Basic(username, password);
        public static AuthToken Basic(string username, string password, string realm) => Driver.AUTH_TOKENS.Basic(username, password, realm);
        public static AuthToken Kerberos(string base64EncodedTicket) => Driver.AUTH_TOKENS.Kerberos(base64EncodedTicket);
        public static AuthToken Custom(string principal, string credentials, string realm, string scheme) => Driver.AUTH_TOKENS.Custom(principal, credentials, realm, scheme);
        public static AuthToken Custom(string principal, string credentials, string realm, string scheme, Dictionary<string, object> parameters) => Driver.AUTH_TOKENS.Custom(principal, credentials, realm, scheme, parameters);
        public static AuthToken Bearer(string token) => Driver.AUTH_TOKENS.Bearer(token);
    }
}
