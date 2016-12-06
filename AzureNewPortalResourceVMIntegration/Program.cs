using System;

namespace AzureNewPortalResourceVMIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string tenantId = "<Enter Directory Id>";
            string clientId = "<Enter Application Id>";
            string secretKey = "<Enter Secret Key>";
            string subscriptionId = "<Enter Subscription Id>";
            string resourceGroupName = "<Enter Resource group name>";
            string virtualMachineName = "<Enter Virtual Machine name>";

            string oauthUrl = "https://login.microsoftonline.com/{0}/oauth2/authorize";
            string serviceResourceId = "https://management.core.windows.net/";

            string authority = string.Format(oauthUrl, tenantId);

            string vmDetailsURL = $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}?api-version=2015-06-15";

            string accessToken = AzureAD.Authenticate(authority, serviceResourceId, clientId, secretKey);

            if (accessToken != null)
            {
                AzureRestClient.ExecuteRequest(accessToken, vmDetailsURL)
                     .ContinueWith((task) =>
                     {
                         Console.WriteLine(task.Result);
                     });

            }
            Console.ReadLine();
        }
    }
}
