using CQRS.Core.Events.Abstract;
using Sales.Domain.Repository;
using Sales.Domain.Repository.Entity;
using Sales.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.EventHandlers
{
    public class CartEventHandler:
        IEventHandler<CartCreatedEvent>,
        IEventHandler<CartItemAddedEvent>,
        IEventHandler<CartItemEditedEvent>,
        IEventHandler<CartItemRemovedEvent>

    {
        private readonly ICartRepository _cartRepository;

        public CartEventHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task Handle(CartCreatedEvent @event, CancellationToken cancellation)
        {
            await _cartRepository.CreateCartForNewClient(@event.Id, @event.ClientId);
        }

        public async Task Handle(CartItemAddedEvent @event, CancellationToken cancellation)
        {
            await _cartRepository.AddItemToCart(new CartItemEntity(@event.ItemId, @event.ProductId, @event.Quantity), @event.Id);
        }

        public async Task Handle(CartItemEditedEvent @event, CancellationToken cancellation)
        {
            await _cartRepository.EditCartItem(@event.ItemId, @event.NewQuantity);
        }

        public async Task Handle(CartItemRemovedEvent @event, CancellationToken cancellation)
        {
            await _cartRepository.RemoveCartItem(@event.ItemId);
        }
    }
}
