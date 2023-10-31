using CQRS.Core.Commands.Abstract;
using Microsoft.AspNetCore.Mvc;
using Promotion.Messages.Commands;

namespace Promotion.Endpoint.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromotionController: ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public PromotionController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddPromotionAsync(PromotionAddCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> EditPromotionAsync(PromotionEditCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }

    }
}
