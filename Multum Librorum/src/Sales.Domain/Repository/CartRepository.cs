using Microsoft.EntityFrameworkCore;
using Sales.Domain.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Repository
{
    public interface ICartRepository
    {
        public Task CreateCartForNewClient(Guid id, Guid clientId);
        public Task AddItemToCart(CartItemEntity cartItemEntity, Guid clientId);
        public Task<CartEntity> GetCartByClientId(Guid clientId);
        public Task<CartEntity> GetCartById(Guid cartId);
        public Task EditCartItem(Guid itemId, int newQuantity);
        public Task RemoveCartItem(Guid itemId);
        public Task UpdateCart(CartEntity cartEntity);
    }

    public class CartRepository : ICartRepository
    {
        private readonly SalesDataContext _salesDataContext;

        public CartRepository(SalesDataContext salesDataContext)
        {
            _salesDataContext = salesDataContext;
        }
        
        public async Task AddItemToCart(CartItemEntity cartItemEntity, Guid cartId)
        {
            var cart = _salesDataContext
                .Carts
                .Include(x=>x.Items)
                .Single(x => x.Id == cartId);
            
            cart.Items.Add(cartItemEntity);
            await _salesDataContext.SaveChangesAsync();
        }

        public async Task CreateCartForNewClient(Guid id, Guid clientId)
        {
            var cart = new CartEntity(id, clientId);
            _salesDataContext.Carts.Add(cart);

            await _salesDataContext.SaveChangesAsync();
        }

        public async Task EditCartItem(Guid itemId, int newQuantity)
        {
            var item = _salesDataContext.CartItems.Single(x => x.Id == itemId);
            item.Quantity = newQuantity;

            _salesDataContext.Update(item);
            await _salesDataContext.SaveChangesAsync();
        }

        public async Task<CartEntity> GetCartByClientId(Guid clientId)
        {
            return await _salesDataContext.Carts
                .Include(x => x.Items)
                .SingleOrDefaultAsync(x => x.ClientId == clientId);
        }

        public async Task<CartEntity> GetCartById(Guid cartId)
        {
            return await _salesDataContext.Carts
                .Include(x => x.Items)
                .SingleOrDefaultAsync(x => x.Id == cartId);
        }

        public async Task RemoveCartItem(Guid itemId)
        {
            var item  = _salesDataContext.CartItems.Single(x => x.Id == itemId);

            _salesDataContext.CartItems.Remove(item);
            await _salesDataContext.SaveChangesAsync();
        }

        public async Task UpdateCart(CartEntity cartEntity)
        {
            _salesDataContext.Carts.Update(cartEntity);
            await _salesDataContext.SaveChangesAsync();
        }
    }
}
