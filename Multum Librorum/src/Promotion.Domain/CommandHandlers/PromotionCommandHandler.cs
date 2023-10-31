using CQRS.Core.Commands.Abstract;
using Marte.EventSourcing.Core.Abstract;
using Promotion.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.Domain.CommandHandlers
{
    public class PromotionCommandHandler :
        ICommandHandler<PromotionAddCommand>,
        ICommandHandler<PromotionEditCommand>,
        ICommandHandler<PromotionEndedCommand>
    {
        private readonly IAggregateReporitory _aggregateReporitory;

        public PromotionCommandHandler(IAggregateReporitory aggregateReporitory)
        {
            _aggregateReporitory = aggregateReporitory;
        }

        public async Task Handle(PromotionAddCommand command, CancellationToken cancellation)
        {
            var promotion = new Aggregates.Promotion(command.Description, command.PromotionInPercentage, command.Products, command.EndDate);
            await _aggregateReporitory.StoreAsync(promotion);
        }

        public async Task Handle(PromotionEditCommand command, CancellationToken cancellation)
        {
            var promotion = await _aggregateReporitory.LoadAsync<Aggregates.Promotion>(command.Id);
            promotion.Edit(command.Description, command.PromotionInPercentage, command.Products, command.EndDate);

            await _aggregateReporitory.StoreAsync(promotion);
        }

        public async Task Handle(PromotionEndedCommand command, CancellationToken cancellation)
        {
            var promotion = await _aggregateReporitory.LoadAsync<Aggregates.Promotion>(command.Id);
            promotion.EndPromotion();

            await _aggregateReporitory.StoreAsync(promotion);
        }
    }
}
