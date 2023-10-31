using CQRS.Core.Commands.Abstract;
using Microsoft.AspNetCore.Mvc;
using Product.Messages.Commands;

namespace Product.Endpoint.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController: ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public BooksController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddBookAsync(AddBookCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("UpdateDetails")]
        public async Task<IActionResult> UpdateBookDetailsAsync(UpdateBookDetailsCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("UpdatePrice")]
        public async Task<IActionResult> UpdateBookPriceAsync(UpdateBookPriceCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("UpdateQuantity")]
        public async Task<IActionResult> UpdateBookQuantityAsync(UpdateBookQuantityCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("HideBook")]
        public async Task<IActionResult> HideBookAsync(HideBookCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("UnHideBook")]
        public async Task<IActionResult> UnHideBookAsync(UnHideBookCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
