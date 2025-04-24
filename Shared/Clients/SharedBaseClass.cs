using Microsoft.Identity.Client;
using S0WISRXX.SharedExternal.Logger;

namespace Shared.Clients
{
    public class SharedBaseClass
    {
        private static IUtilityLogger _logger;

        public SharedBaseClass(IUtilityLogger logger)
        {
            _logger = logger;
        }

        /// <summary> 
        /// Use the MSAL.NET library to request an Azure AD token using the application's own identity
        /// </summary>
        /// <param name="tenantId">The directory (tenant) ID for the application registered in Azure AD</param>
        /// <param name="clientId">The application (client) ID for the application registered in Azure AD</param>
        /// <param name="clientSecret">The client secret created for the application in Azure AD</param>
        /// <param name="scopes">Scopes defined by the app.  With client credentials flows, the scope is always in the format "resource/.default"</param>
        /// <remarks>Alternative scope values include https://graph.microsoft.com/.default if access to the graph api is needed</remarks>
        /// <returns>The AD access token</returns>
        public async Task<(string, string)> RequestAzureADTokenUsingApplicationIdentityWithExpiry(string tenantId, string clientId,
            string clientSecret, string[] scopes, string application)
        {
            _logger.LogInformation("");
            _logger.LogInformation($"Attempting to retrieve Azure AD token for {application} for authentication...");
            IConfidentialClientApplication app;

            try
            {
                // Check values to ensure that we have what is neccessary for operation
                var exceptionMessage = "";
                if (string.IsNullOrEmpty(tenantId))
                    exceptionMessage += "TenantId was null while generating token. ";
                if (string.IsNullOrEmpty(clientId))
                    exceptionMessage += "ClientId was null while generating token. ";
                if (scopes == null)
                    exceptionMessage += "There were no scopes while generating token. ";

                // Checks exception message
                if (!string.IsNullOrEmpty(exceptionMessage))
                    throw new ArgumentException(exceptionMessage);

                string authority = $"https://login.microsoftonline.com/{tenantId}/";
                app = ConfidentialClientApplicationBuilder.Create(clientId)
                                                          .WithClientSecret(clientSecret)
                                                          .WithAuthority(new Uri(authority))
                                                          .Build();
                var result = await app.AcquireTokenForClient(scopes).ExecuteAsync();

                if (result != null)
                {
                    _logger.LogInformation($"Azure AD token for {application} successfully retrieved.");
                    _logger.LogInformation("");
                    return (result.AccessToken, result.ExpiresOn.ToString());
                }
                return (null, null);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error has occurred while retrieving Azure AD token. Error message: {ex.Message}");
            }
        }

        /// <summary> 
        /// Use the MSAL.NET library to request an Azure AD token using the application's own identity
        /// </summary>
        /// <param name="tenantId">The directory (tenant) ID for the application registered in Azure AD</param>
        /// <param name="clientId">The application (client) ID for the application registered in Azure AD</param>
        /// <param name="clientSecret">The client secret created for the application in Azure AD</param>
        /// <param name="scopes">Scopes defined by the app.  With client credentials flows, the scope is always in the format "resource/.default"</param>
        /// <remarks>Alternative scope values include https://graph.microsoft.com/.default if access to the graph api is needed</remarks>
        /// <returns>The AD access token</returns>
        public async Task<(string, string)> RefreshTokenWithExpiry(string tenantId, string clientId, string clientSecret, string[] scopes)
        {
            IConfidentialClientApplication app;

            try
            {
                // Check values to ensure that we have what is neccessary for operation
                var exceptionMessage = "";
                if (string.IsNullOrEmpty(tenantId))
                    exceptionMessage += "TenantId was null while generating token. ";
                if (string.IsNullOrEmpty(clientId))
                    exceptionMessage += "ClientId was null while generating token. ";
                if (scopes == null)
                    exceptionMessage += "There were no scopes while generating token. ";

                // Checks exception message
                if (!string.IsNullOrEmpty(exceptionMessage))
                    throw new ArgumentException(exceptionMessage);

                string authority = $"https://login.microsoftonline.com/{tenantId}/";
                app = ConfidentialClientApplicationBuilder.Create(clientId)
                                                          .WithClientSecret(clientSecret)
                                                          .WithAuthority(new Uri(authority))
                                                          .Build();
                var result = await app.AcquireTokenForClient(scopes).ExecuteAsync();

                if (result != null)
                {
                    return (result.AccessToken, result.ExpiresOn.ToString());
                }
                return (null, null);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error has occurred while retrieving Azure AD token. Error message: {ex.Message}");
            }
        }

        public void Wait()
        {
            //added thread sleep to generate log forwarder for uim.
            _logger.LogInformation($"Application will sleep for 2 seconds re-running.");
            Thread.Sleep(2000);
        }


        // This logs jobs closing due to successful runs or errors
        public void ShutDownJob(int exitCode, bool wasSuccessful, string jobName, string errorMessage = null)
        {
            //added thread sleep to generate log forwarder for uim.
            _logger.LogInformation($"Application will sleep for 2 seconds before shutdown.");
            Thread.Sleep(2000);

            if (wasSuccessful)
                _logger.LogInformation($"The '{jobName}' job finished successfully at {DateTime.Now}.");
            else
                _logger.LogError($"The '{jobName}' job was terminated with an error. Error message: {errorMessage}");

            if (jobName != null)
                _logger.LogInformation($"Shutting down '{jobName}' job...");
            else
                _logger.LogError($"Shutting down job...");

            Environment.ExitCode = exitCode;
            Environment.Exit(exitCode);
        }

        // This logs jobs closing due to close event handlers or nothing being available for processing
        public void ShutDownJob(int exitCode, string jobName = null)
        {
            //added thread sleep to generate log forwarder for uim.
            _logger.LogInformation($"Application will sleep for 2 seconds before shutdown.");
            Thread.Sleep(2000);

            if (jobName != null)
                _logger.LogInformation($"Shutting down '{jobName}' job...");
            else
                _logger.LogError($"Shutting down job...");

            Environment.ExitCode = exitCode;
            Environment.Exit(exitCode);
        }

        ///// <summary>
        ///// Gets all MDM warehouses
        ///// </summary>
        ///// <returns>List of warehouses</returns>
        //public async Task<List<WarehouseInstance>> GetAllWarehouseInstances(HttpClient httpClient, string endpoint)
        //{
        //    try
        //    {
        //        var response = await httpClient.GetAsync(endpoint);
        //        response.EnsureSuccessStatusCode();

        //        var result = await response.Content.ReadAsStringAsync();
        //        if (result == "{}")
        //            return null;

        //        var apiResponse = JsonConvert.DeserializeObject<GenericObject>(result);
        //        var warehouseInstances = JsonConvert.DeserializeObject<List<WarehouseInstance>>(apiResponse.Result?.ToString());

        //        return warehouseInstances;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"An exception occurred in GetAllWarehouseInstances(). Error message: {ex.Message}.");
        //        throw;
        //    }
        //}
    }
}