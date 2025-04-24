using S0WISRXX.SharedExternal.Logger;

namespace Shared.Clients;

public class PurchasingClient : IDisposable
{
    private readonly object _locker = new object();
    private volatile HttpClient _client = new HttpClient();

    private readonly string _baseUrl;
    private DateTime _authTokenExpiry;
    private readonly string _tenantId;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string[] _scopes;
    private string _authToken;
    private readonly IUtilityLogger _logger;
    private readonly SharedBaseClass _sharedBaseClass;

    public PurchasingClient(IUtilityLogger logger, string baseUrl, string tenantId, string clientId, string clientSecret, string[] scopes)
    {
        _baseUrl = baseUrl;
        _tenantId = tenantId;
        _clientId = clientId;
        _clientSecret = clientSecret;
        _scopes = scopes;
        _logger = logger;
        _sharedBaseClass = new SharedBaseClass(_logger);

        _logger.LogInformation($"#####---------------------- Initializing Purchasing API -----------------------#####");
        var apiToken = _sharedBaseClass.RequestAzureADTokenUsingApplicationIdentityWithExpiry(_tenantId, _clientId, _clientSecret, _scopes, "Purchasing API").Result;
        _authTokenExpiry = DateTime.Parse(apiToken.Item2);
        _authToken = apiToken.Item1;
        _client.Timeout = TimeSpan.FromMinutes(5);
        _client.BaseAddress = new Uri(_baseUrl);
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authToken);
        _logger.LogInformation($"#####--------------------------------------------------------------------------#####");
    }


    public HttpClient Client
    {
        get
        {
            if (_client == null || _client != null && _authTokenExpiry.AddMinutes(-5) <= DateTime.Now)
            {
                lock (_locker)
                {
                    if (_client == null || _client != null && _authTokenExpiry.AddMinutes(-5) <= DateTime.Now)
                    {
                        _client = new HttpClient();
                        _client.Timeout = TimeSpan.FromMinutes(5);
                        _client.BaseAddress = new Uri(_baseUrl);

                        var Token = _sharedBaseClass.RefreshTokenWithExpiry(_tenantId, _clientId, _clientSecret, _scopes).Result;
                        _authToken = Token.Item1;
                        _authTokenExpiry = DateTime.Parse(Token.Item2);
                        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authToken);
                    }
                }
            }
            return _client;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_client != null)
            {
                _client.Dispose();
            }

            _client = null;
        }
    }
}
