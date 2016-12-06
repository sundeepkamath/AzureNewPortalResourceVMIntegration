using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;

namespace AzureNewPortalResourceVMIntegration
{
    public class AzureAD
    {
        internal static string Authenticate(string authority, 
            string serviceResourceId, string clientId, string secretKey)
        {
            AuthenticationContext authContext = new AuthenticationContext(authority, true);
            AuthenticationResult authResult = null;

            try
            {
                authResult = authContext
                    .AcquireTokenAsync(serviceResourceId, 
                                new ClientCredential(clientId, secretKey)).Result;

                if (authResult != null)
                    Console.WriteLine("Auth Token: {0}", authResult.AccessToken);
                else
                    throw new Exception("Azure AD authentication failed.");

            }
            catch (AdalException adEx)
            {
                Console.WriteLine("Exception: {0}", adEx);
            }

            return authResult.AccessToken;
        }
    }
}
