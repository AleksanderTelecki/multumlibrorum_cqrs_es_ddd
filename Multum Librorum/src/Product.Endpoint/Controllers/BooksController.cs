using CQRS.Core.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Messages.Commands;

namespace Product.Endpoint.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController: ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IMediator mediator;

        public BooksController(ILogger<BooksController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddBookAsync(AddBookCommand command)
        {
            await mediator.Send(command);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
