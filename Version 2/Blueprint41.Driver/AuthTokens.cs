using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public sealed class AuthToken
    {
        internal AuthToken(object value)
        {
            Value = value;
        }
        internal object Value { get; private set; }

        //
        // Summary:
        //     Gets an authentication token that can be used to connect to Neo4j instances with
        //     auth disabled. This will only work if authentication is disabled on the Neo4j
        //     Instance we are connecting to.
        //
        // Remarks:
        //     Neo4j.Driver.GraphDatabase.Driver(System.String,Neo4j.Driver.IAuthToken,System.Action{Neo4j.Driver.ConfigBuilder})
        public static AuthToken None => Driver.AUTH_TOKENS.None;

        //
        // Summary:
        //     The basic authentication scheme, using a username and a password.
        //
        // Parameters:
        //   username:
        //     This is the "principal", identifying who this token represents.
        //
        //   password:
        //     This is the "credential", proving the identity of the user.
        //
        // Returns:
        //     An authentication token that can be used to connect to Neo4j.
        //
        // Remarks:
        //     Neo4j.Driver.GraphDatabase.Driver(System.String,Neo4j.Driver.IAuthToken,System.Action{Neo4j.Driver.ConfigBuilder})
        public static AuthToken Basic(string username, string password) => Driver.AUTH_TOKENS.Basic(username, password);

        //
        // Summary:
        //     The basic authentication scheme, using a username and a password.
        //
        // Parameters:
        //   username:
        //     This is the "principal", identifying who this token represents.
        //
        //   password:
        //     This is the "credential", proving the identity of the user.
        //
        //   realm:
        //     This is the "realm", specifies the authentication provider. If none is given,
        //     default to be decided by the server.
        //
        // Returns:
        //     An authentication token that can be used to connect to Neo4j.
        //
        // Remarks:
        //     Neo4j.Driver.GraphDatabase.Driver(System.String,Neo4j.Driver.IAuthToken,System.Action{Neo4j.Driver.ConfigBuilder})
        public static AuthToken Basic(string username, string password, string realm) => Driver.AUTH_TOKENS.Basic(username, password, realm);

        //
        // Summary:
        //     The kerberos authentication scheme, using a base64 encoded ticket.
        //
        // Parameters:
        //   base64EncodedTicket:
        //     A base64 encoded service ticket.
        //
        // Returns:
        //     an authentication token that can be used to connect to Neo4j.
        //
        // Remarks:
        //     Neo4j.Driver.GraphDatabase.Driver(System.String,Neo4j.Driver.IAuthToken,System.Action{Neo4j.Driver.ConfigBuilder})
        public static AuthToken Kerberos(string base64EncodedTicket) => Driver.AUTH_TOKENS.Kerberos(base64EncodedTicket);

        //
        // Summary:
        //     Gets an authentication token that can be used to connect to Neo4j instances with
        //     auth disabled. This will only work if authentication is disabled on the Neo4j
        //     Instance we are connecting to.
        //
        // Parameters:
        //   principal:
        //     This is used to identify who this token represents.
        //
        //   credentials:
        //     This is credentials authenticating the principal.
        //
        //   realm:
        //     This is the "realm", specifies the authentication provider.
        //
        //   scheme:
        //     This is the authentication scheme, specifying what kind of authentication that
        //     should be used.
        //
        // Returns:
        //     An authentication token that can be used to connect to Neo4j.
        //
        // Remarks:
        //     Neo4j.Driver.GraphDatabase.Driver(System.String,Neo4j.Driver.IAuthToken,System.Action{Neo4j.Driver.ConfigBuilder})
        public static AuthToken Custom(string principal, string credentials, string realm, string scheme) => Driver.AUTH_TOKENS.Custom(principal, credentials, realm, scheme);

        //
        // Summary:
        //     Gets an authentication token that can be used to connect to Neo4j instances with
        //     auth disabled. This will only work if authentication is disabled on the Neo4j
        //     Instance we are connecting to.
        //
        // Parameters:
        //   principal:
        //     This is used to identify who this token represents.
        //
        //   credentials:
        //     This is credentials authenticating the principal.
        //
        //   realm:
        //     This is the "realm", specifies the authentication provider.
        //
        //   scheme:
        //     This is the authentication scheme, specifying what kind of authentication that
        //     should be used.
        //
        //   parameters:
        //     Extra parameters to be sent along the authentication provider. If none is given,
        //     then no extra parameters will be added.
        //
        // Returns:
        //     An authentication token that can be used to connect to Neo4j.
        //
        // Remarks:
        //     Neo4j.Driver.GraphDatabase.Driver(System.String,Neo4j.Driver.IAuthToken,System.Action{Neo4j.Driver.ConfigBuilder})
        public static AuthToken Custom(string principal, string credentials, string realm, string scheme, Dictionary<string, object> parameters) => Driver.AUTH_TOKENS.Custom(principal, credentials, realm, scheme, parameters);

        //
        // Summary:
        //     The bearer authentication scheme, using a base64 encoded token, such as those
        //     supplied by SSO providers. Gets an authentication token that can be used to connect
        //     to Neo4j.
        //
        // Parameters:
        //   token:
        //     Base64 encoded token
        //
        // Returns:
        //     An authentication token that can be used to connect to Neo4j.
        //
        // Remarks:
        //     Neo4j.Driver.GraphDatabase.Driver(System.String,Neo4j.Driver.IAuthToken,System.Action{Neo4j.Driver.ConfigBuilder})
        public static AuthToken Bearer(string token) => Driver.AUTH_TOKENS.Bearer(token);
    }
}
