using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json;
using CQRS.Communication.DTOs;
using CQRS.Core.Commands;
using CQRS.Core.Commands.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Communication.Controllers;

[ApiController]
[Route("[controller]")]
public class RestCommandController: ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private static readonly MethodInfo DispatchMethodInfo = typeof(CommandDispatcher).GetMethod(nameof(CommandDispatcher.Dispatch));
    private static readonly ConcurrentDictionary<string, Type> CommandTypeCache = new();
    
    public RestCommandController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }
    
    [HttpPost]
    public async Task<IActionResult> DispatchRestCommand([FromBody] CqrsMessage commandMessage)
    {
        var commandType = CommandTypeCache.GetOrAdd(commandMessage.Type, Type.GetType);
        var command = JsonSerializer.Deserialize(commandMessage.Data, commandType);

        if (command == null)
            return BadRequest("Command type was not specified or passed incorrect command input.");

        await DispatchCommand(command, commandType);

        return Ok();
    }

    private async Task DispatchCommand(object command, Type commandType)
    {
        var dispatchMethod = DispatchMethodInfo.MakeGenericMethod(commandType);
        dynamic task = dispatchMethod.Invoke(_commandDispatcher, new[] { command, CancellationToken.None });
        await task;
    }
    
}