using CQRS.Core.Commands.Abstract;
using Microsoft.AspNetCore.Mvc;
using Client.Messages.Commands;

namespace Client.Endpoint.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientController: ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public ClientController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterClientAsync(RegisterUserCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("UpdateProfile")]
        public async Task<IActionResult> UpdateClientProfileAsync(UpdateProfileInfoCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangeClientPasswordAsync(ChangeUserPasswordCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
