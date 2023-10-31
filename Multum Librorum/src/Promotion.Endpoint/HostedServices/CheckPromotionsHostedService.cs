using CQRS.Core.Commands;
using CQRS.Core.Commands.Abstract;
using Promotion.Domain;
using Promotion.Domain.Repository;
using Promotion.Messages.Commands;

namespace Promotion.Endpoint.HostedServices
{
    public class CheckPromotionsHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceScopeFactory _scopeFactory;

        public CheckPromotionsHostedService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CheckPromotions, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
            return Task.CompletedTask;
        }

        private async void CheckPromotions(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var promotionRepository = scope.ServiceProvider.GetRequiredService<IPromotionRepository>();
                var commandDispatcher = scope.ServiceProvider.GetRequiredService<ICommandDispatcher>();

                var promotions = await promotionRepository.GetAllPromotions();

                foreach (var promotion in promotions)
                {
                    if (promotion.EndDate < DateTime.Now && promotion.IsActive)
                    {
                        await commandDispatcher.Dispatch(new PromotionEndedCommand { Id = promotion.Id });
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
