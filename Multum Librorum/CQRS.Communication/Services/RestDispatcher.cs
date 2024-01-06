using System.Net.Http.Json;
using CQRS.Communication.Abstract;
using CQRS.Communication.DTOs;
using CQRS.Communication.Enums;
using CQRS.Communication.Options;
using CQRS.Core.Commands.Abstract;
using CQRS.Core.Queries.Abstract;
using Microsoft.Extensions.Options;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CQRS.Communication.Services;

// TODO: REFACTOR

public class RestDispatcher: IRestDispatcher
{
    private readonly IOptions<RestDispatcherOptions> _restDispatcherOptions;
    private readonly IHttpClientFactory _clientFactory;

    public RestDispatcher(IOptions<RestDispatcherOptions> restDispatcherOptions, IHttpClientFactory clientFactory)
    {
        _restDispatcherOptions = restDispatcherOptions;
        _clientFactory = clientFactory;
    }
    
    public async Task<TResult> DispatchQuery<TResult>(IQuery<TResult> query, EndpointEnum endpoint)
    {
        var httpClient = GetClient(endpoint);

        var queryData = JsonSerializer.Serialize(query, query.GetType());
        var cqrsMessageWrap =  new CqrsMessage(query.Type, queryData);
        
        var response = await httpClient.PostAsJsonAsync("RestQuery", cqrsMessageWrap);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TResult>();
    }
    
    public async Task DispatchCommand<TCommand>(TCommand command, EndpointEnum endpoint) where TCommand : ICommand
    {
        var httpClient = GetClient(endpoint);
        
        var commandData = JsonSerializer.Serialize(command, command.GetType());
        var cqrsMessageWrap = new CqrsMessage(command.Type, commandData);
        
        var response = await httpClient.PostAsJsonAsync("RestCommand", cqrsMessageWrap);
        response.EnsureSuccessStatusCode();
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