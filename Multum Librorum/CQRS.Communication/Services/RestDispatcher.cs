using System.Text.Json;
using CQRS.Communication.Abstract;
using CQRS.Communication.Enums;
using CQRS.Communication.Options;
using CQRS.Core.Queries.Abstract;
using Microsoft.Extensions.Options;

namespace CQRS.Communication.Services;

public class RestDispatcher: IRestDispatcher
{
    private readonly IOptions<RestDispatcherOptions> _restDispatcherOptions;
    private readonly IHttpClientFactory _clientFactory;

    public RestDispatcher(IOptions<RestDispatcherOptions> restDispatcherOptions, IHttpClientFactory clientFactory)
    {
        _restDispatcherOptions = restDispatcherOptions;
        _clientFactory = clientFactory;
    }
    
    public async Task<TResult> DispatchQuery<TResult>(Query<TResult> query, EndpointEnum endpoint)
    {
        var httpClient = GetClient(endpoint);
        
        var jsonContent = JsonSerializer.Serialize(query, query.GetType());
        var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
        
        var response = await httpClient.PostAsync("RestQuery", content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TResult>();
    }

    private HttpClient GetClient(EndpointEnum endpoint)
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