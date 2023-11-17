using CQRS.Core.Commands.Abstract;
using Microsoft.AspNetCore.Mvc;
using User.Messages.Commands;

namespace User.Endpoint.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public UserController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUserAsync(RegisterUserCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("UpdateProfile")]
        public async Task<IActionResult> UpdateUserProfileAsync(UpdateProfileInfoCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangeUserPasswordAsync(ChangeUserPasswordCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
