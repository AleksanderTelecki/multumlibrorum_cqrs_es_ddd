using System.Collections.Concurrent;
using System.Reflection;
using CQRS.Core.Queries;
using CQRS.Core.Queries.Abstract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CQRS.Communication.Controllers;

[ApiController]
[Route("[controller]")]
public class RestQueryController: ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;

    public RestQueryController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }
    
    private static readonly MethodInfo DispatchMethodInfo = typeof(QueryDispatcher).GetMethod(nameof(QueryDispatcher.Dispatch));
    
    private static readonly ConcurrentDictionary<string, Type> QueryTypeCache = new();
    private static readonly ConcurrentDictionary<Type, Type> ResultTypeCache = new();

    [HttpPost]
    public async Task<IActionResult> DispatchRestQuery([FromBody] object queryData)
    {
        try
        {
            var jsonParsed = JObject.Parse(queryData.ToString());
            var typeName = jsonParsed["QueryType"]?.ToString();
            
            if (string.IsNullOrWhiteSpace(typeName))
            {
                return BadRequest("Query type not specified.");
            }
            
            var queryType = QueryTypeCache.GetOrAdd(typeName, Type.GetType);

            if (queryType == null)
            {
                return BadRequest("Query type not found.");
            }

            var resultType = ResultTypeCache.GetOrAdd(queryType, qt => qt.GetInterface("IQuery`1")?.GetGenericArguments()[0]);
            
            if (resultType == null)
            {
                return BadRequest("Invalid query type.");
            }

            var query = JsonConvert.DeserializeObject(queryData.ToString(), queryType);
            
            var result = await DispatchQuery(queryType, resultType, query);
            return Ok(result);
        }
        catch (JsonSerializationException)
        {
            return BadRequest("Invalid query data.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private async Task<object> DispatchQuery(Type queryType, Type resultType, object query)
    {
        var genericDispatchMethod = DispatchMethodInfo.MakeGenericMethod(resultType);
        dynamic task = genericDispatchMethod.Invoke(_queryDispatcher, new[] { query, CancellationToken.None });
        
        await task;
        return task.Result;
    }
}