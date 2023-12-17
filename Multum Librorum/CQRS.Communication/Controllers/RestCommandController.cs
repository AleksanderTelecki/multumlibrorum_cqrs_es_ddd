using CQRS.Core.Commands.Abstract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CQRS.Communication.Controllers;

[ApiController]
[Route("[controller]")]
public class RestCommandController: ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public RestCommandController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }
    
    [HttpPost]
    public async Task<IActionResult> DispatchRestCommand([FromBody] object command)
    {
        var x = 5;
        //await _commandDispatcher.Dispatch(command);

        return Ok();
    }
}