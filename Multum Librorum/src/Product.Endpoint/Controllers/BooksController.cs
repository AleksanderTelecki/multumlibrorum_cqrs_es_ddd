using CQRS.Core.Commands.Abstract;
using Microsoft.AspNetCore.Mvc;
using Product.Messages.Commands;

namespace Product.Endpoint.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController: ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public BooksController(ILogger<BooksController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddBookAsync(AddBookCommand command)
        {
            await _commandDispatcher.Dispatch(command);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
