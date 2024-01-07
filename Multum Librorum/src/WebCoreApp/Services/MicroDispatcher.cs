using CQRS.Communication.Enums;
using CQRS.Communication.Options;
using Microsoft.Extensions.Options;
using WebCoreApp.Abstract;

namespace WebCoreApp.Services;

public class MicroDispatcher: IMicroDispatcher
{
    private readonly IOptions<RestDispatcherOptions> _restDispatcherOptions;
    private readonly IHttpClientFactory _clientFactory;
    
    public MicroDispatcher(IOptions<RestDispatcherOptions> restDispatcherOptions, IHttpClientFactory clientFactory)
    {
        _restDispatcherOptions = restDispatcherOptions;
        _clientFactory = clientFactory;
    }
    
    public HttpClient GetClient(EndpointEnum endpoint)
    {
        var client = _clientFactory.CreateClient();
        if (_restDispatcherOptions.Value.EndpointRoutes.TryGetValue(endpoint, out var baseUrl))
        {
            client.BaseAddress = new Uri(baseUrl);
        }
        else
        {
            throw new InvalidOperationException($"Base URL not configured for endpoint: {endpoint}");
        }

        return client;
    }
}