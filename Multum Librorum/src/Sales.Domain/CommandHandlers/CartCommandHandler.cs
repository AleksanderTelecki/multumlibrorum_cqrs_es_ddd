using CQRS.Core.Commands.Abstract;
using Marte.EventSourcing.Core.Abstract;
using Sales.Domain.Aggregates;
using Sales.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.CommandHandlers
{
    public class CartCommandHandler : 
        ICommandHandler<AddItemToCartCommand>,
        ICommandHandler<RemoveItemFromCartCommand>,
        ICommandHandler<EditCartItemCommand>,
        ICommandHandler<CreateCartCommand>

    {

        private readonly IAggregateRepository _aggregateRepository;

        public CartCommandHandler(IAggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public async Task Handle(AddItemToCartCommand command, CancellationToken cancellation)
        {
            var cart =  await _aggregateRepository.LoadAsync<Cart>(command.Id);
            cart.AddItem(command.ProductId, command.Quantity);

            await _aggregateRepository.StoreAsync(cart);
        }

        public async Task Handle(RemoveItemFromCartCommand command, CancellationToken cancellation)
        {
            var cart = await _aggregateRepository.LoadAsync<Cart>(command.Id);
            cart.RemoveItem(command.ProductId);

            await _aggregateRepository.StoreAsync(cart);
        }

        public async Task Handle(EditCartItemCommand command, CancellationToken cancellation)
        {
            var cart = await _aggregateRepository.LoadAsync<Cart>(command.Id);
            cart.EditItemQuantity(command.ProductId, command.NewQuantity);

            await _aggregateRepository.StoreAsync(cart);
        }

        public async Task Handle(CreateCartCommand command, CancellationToken cancellation)
        {
            var cart = new Cart(command.ClientId);
            await _aggregateRepository.StoreAsync(cart);
        }
    }
}
