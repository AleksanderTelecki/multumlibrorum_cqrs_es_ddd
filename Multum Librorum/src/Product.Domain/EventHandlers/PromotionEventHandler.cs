using CQRS.Core.Commands;
using CQRS.Core.Commands.Abstract;
using CQRS.Core.Events.Abstract;
using Product.Domain.Repository;
using Product.Messages.Commands;
using Promotion.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.EventHandlers
{
    public class PromotionEventHandler :
        IEventHandler<PromotionAddedEvent>,
        IEventHandler<PromotionEditedEvent>,
        IEventHandler<PromotionEndedEvent>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public PromotionEventHandler(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public async Task Handle(PromotionAddedEvent @event, CancellationToken cancellation)
        {
            foreach (var bookId in @event.Products)
            {
                await _commandDispatcher.Dispatch(new AddBookPromotionCommand { Id = bookId, PromotedPercentage = @event.PromotionInPercentage });
            }
        }

        public async Task Handle(PromotionEditedEvent @event, CancellationToken cancellation)
        {
            foreach (var bookId in @event.PreviousProducts)
            {
                await _commandDispatcher.Dispatch(new RemoveBookPromotionCommand { Id = bookId});
            }

            foreach (var bookId in @event.NewProducts)
            {
                await _commandDispatcher.Dispatch(new AddBookPromotionCommand { Id = bookId, PromotedPercentage = @event.PromotionInPercentage });
            }
        }

        public async Task Handle(PromotionEndedEvent @event, CancellationToken cancellation)
        {
            foreach (var bookId in @event.Products)
            {
                await _commandDispatcher.Dispatch(new RemoveBookPromotionCommand { Id = bookId });
            }
        }
    }
}
