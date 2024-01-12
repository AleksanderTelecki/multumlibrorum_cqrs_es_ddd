using Microsoft.EntityFrameworkCore;
using Promotion.Domain.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.Domain.Repository
{
    public interface IPromotionRepository
    {
        public Task CreatePromotion(PromotionEntity promotionEntity);
        public Task<PromotionEntity> GetPromotion(Guid id);
        public Task UpdatePromotion(PromotionEntity promotionEntity);
        public Task<List<PromotionEntity>> GetAllPromotions();

    }

    public class PromotionRepository : IPromotionRepository
    {
        private readonly PromotionDomainDataContext _promotionDomainDataContext;

        public PromotionRepository(PromotionDomainDataContext promotionDomainDataContext)
        {
            _promotionDomainDataContext = promotionDomainDataContext;
        }

        public async Task CreatePromotion(PromotionEntity promotionEntity)
        {
            _promotionDomainDataContext.Promotions.Add(promotionEntity);
            await _promotionDomainDataContext.SaveChangesAsync();
        }

        public async Task<PromotionEntity> GetPromotion(Guid id)
        {
            return await _promotionDomainDataContext.Promotions.Include(x=>x.Products).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdatePromotion(PromotionEntity promotionEntity)
        {
            _promotionDomainDataContext.Promotions.Update(promotionEntity);
            await _promotionDomainDataContext.SaveChangesAsync();
        }

        public async Task<List<PromotionEntity>> GetAllPromotions()
        {
            return await _promotionDomainDataContext.Promotions.Include(x=>x.Products).AsNoTracking().ToListAsync();
        }
    }
}
