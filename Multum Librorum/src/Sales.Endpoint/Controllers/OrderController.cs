using CQRS.Core.Commands.Abstract;
using Microsoft.AspNetCore.Mvc;
using Sales.Messages.Commands;

namespace Sales.Endpoint.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    
    public OrderController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }
    
    [HttpPost("CreateOrder")]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
    {
        await _commandDispatcher.Dispatch(command);
        return StatusCode(StatusCodes.Status200OK);
    }
}