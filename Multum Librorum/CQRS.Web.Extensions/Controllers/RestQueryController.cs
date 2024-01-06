using System.Collections.Concurrent;
using System.Reflection;
using CQRS.Communication.DTOs;
using CQRS.Core.Queries;
using CQRS.Core.Queries.Abstract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CQRS.Web.Extensions.Controllers;

// TODO: Refactor

[ApiController]
[Route("[controller]")]
public class RestQueryController: ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    
    private static readonly MethodInfo DispatchMethodInfo = typeof(QueryDispatcher).GetMethod(nameof(QueryDispatcher.Dispatch));
    
    private static readonly ConcurrentDictionary<string, Type> QueryTypeCache = new();
    
    private static readonly ConcurrentDictionary<Type, Type> ResultTypeCache = new();
    public RestQueryController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }
    
    [HttpPost]
    public async Task<IActionResult> DispatchRestQuery([FromBody] CqrsMessage queryMessage)
    {
        var queryType = QueryTypeCache.GetOrAdd(queryMessage.Type, Type.GetType);
        
        var resultType = ResultTypeCache.GetOrAdd(queryType,
            qt => qt.GetInterface("IQuery`1")?.GetGenericArguments()[0]);

        if (resultType == null)
            return BadRequest("Invalid query type.");

        var query = JsonConvert.DeserializeObject(queryMessage.Data, queryType);
        var result = await DispatchQuery(queryType, resultType, query);

        return Ok(result);
    }

    private async Task<object> DispatchQuery(Type queryType, Type resultType, object query)
    {
        var genericDispatchMethod = DispatchMethodInfo.MakeGenericMethod(resultType);
        dynamic task = genericDispatchMethod.Invoke(_queryDispatcher, new[] { query, CancellationToken.None });
        
        await task;
        return task.Result;
    }
}