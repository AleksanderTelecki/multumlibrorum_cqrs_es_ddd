using Promotion.Domain.Repository;

namespace Promotion.Endpoint.HostedServices
{
    public class CheckPromotionsHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IPromotionRepository promotionRepository;

        public CheckPromotionsHostedService(IPromotionRepository promotionRepository)
        {
            this.promotionRepository = promotionRepository;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CheckPromotions, null, TimeSpan.Zero, TimeSpan.FromMinutes(30));
            return Task.CompletedTask;
        }

        private async void CheckPromotions(object state)
        {
            var promotions = await promotionRepository.GetAllPromotions();

            foreach (var promotion in promotions) 
            {
                if (promotion.EndDate < DateTime.Now)
                {
                    // wyłować komędę informująca o zakończeniu promocji
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
