using Microsoft.EntityFrameworkCore;
using Sales.Domain.Aggregates;
using Sales.Domain.Repository.Entity;
using Sales.Messages.Enums;
using Sales.Messages.Models;

namespace Sales.Domain.Repository;


public interface IOrderRepository
{
    public Task CreateOrderWithCart(CartEntity cartEntity);
    public Task CreateOrderForClient(Guid orderId, Guid clientId);
    public Task ChangeOrderState(Guid orderId, OrderState newOrderState);
    public Task AddProductsToOrder(Guid orderId, ICollection<OrderItem> orderItems);
    public Task<List<OrderEntity>> GetOrdersByClientId(Guid clientId);
    public Task<OrderEntity> GetOrdersByOrderId(Guid orderId);
    public Task<List<OrderEntity>> GetOrders();
    public Task UpdateOrder(OrderEntity orderEntity);
}


public class OrderRepository: IOrderRepository
{
    private readonly SalesDataContext _salesDataContext;

    public OrderRepository(SalesDataContext salesDataContext)
    {
        _salesDataContext = salesDataContext;
    }
    
    public async Task CreateOrderWithCart(CartEntity cartEntity)
    {
        var orderEntity = new OrderEntity(cartEntity);

        foreach (var cartItem in cartEntity.Items)
        {
            orderEntity.Items.Add(new OrderItemEntity(cartItem));
        }

        _salesDataContext.Orders.Add(orderEntity);

        await _salesDataContext.SaveChangesAsync();
    }

    public async Task CreateOrderForClient(Guid orderId, Guid clientId)
    {
        var orderEntity = new OrderEntity(orderId, clientId);
        
        _salesDataContext.Orders.Add(orderEntity);
        await _salesDataContext.SaveChangesAsync();
    }

    public async Task ChangeOrderState(Guid orderId, OrderState newOrderState)
    {
        var order = _salesDataContext
            .Orders
            .Single(x => x.Id == orderId);

        order.State = newOrderState;
        await _salesDataContext.SaveChangesAsync();
    }

    public async Task AddProductsToOrder(Guid orderId, ICollection<OrderItem> orderItems)
    {
        var order = _salesDataContext
            .Orders
            .Include(x=>x.Items)
            .Single(x => x.Id == orderId);

        foreach (var orderItemEntity in orderItems.Select(x=> new OrderItemEntity() {Id = x.Id, ProductId = x.ProductId, Quantity = x.Quantity}))
        {
            order.Items.Add(orderItemEntity);
        }
        
        await _salesDataContext.SaveChangesAsync();
    }

    public async Task<List<OrderEntity>> GetOrdersByClientId(Guid clientId)
    {
        return await _salesDataContext.Orders
            .Include(x => x.Items)
            .Where(x => x.ClientId == clientId).ToListAsync();
    }

    public async Task<OrderEntity> GetOrdersByOrderId(Guid orderId)
    {
        return await _salesDataContext.Orders
            .Include(x => x.Items)
            .SingleOrDefaultAsync(x => x.Id == orderId);
    }

    public async Task<List<OrderEntity>> GetOrders()
    {
        return await _salesDataContext.Orders
            .Include(x => x.Items).ToListAsync();
    }

    public async Task UpdateOrder(OrderEntity orderEntity)
    {
        _salesDataContext.Update(orderEntity);
        await _salesDataContext.SaveChangesAsync();
    }
}