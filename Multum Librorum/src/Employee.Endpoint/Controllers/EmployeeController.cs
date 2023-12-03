using CQRS.Core.Commands.Abstract;
using Employee.Messages.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Endpoint.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public EmployeeController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterClientAsync(RegisterEmployeeCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("UpdateName")]
        public async Task<IActionResult> UpdateClientProfileAsync(UpdateEmployeeNameCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangeClientPasswordAsync(ChangeEmployeePasswordCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("ChangeRole")]
        public async Task<IActionResult> ChangeClientPasswordAsync(ChangeEmployeeRoleCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
