using CQRS.Core.Commands.Abstract;
using Microsoft.AspNetCore.Mvc;
using Product.Messages.Commands;
using Sales.Messages.Commands;

namespace Sales.Endpoint.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public CartController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("AddItemToCart")]
        public async Task<IActionResult> AddItemToCart(AddItemToCartCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("EditCartItem")]
        public async Task<IActionResult> EditCartItem(EditCartItemCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("RemoveItemFromCart")]
        public async Task<IActionResult> RemoveCartItem(RemoveItemFromCartCommand command)
        {
            await _commandDispatcher.Dispatch(command);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
