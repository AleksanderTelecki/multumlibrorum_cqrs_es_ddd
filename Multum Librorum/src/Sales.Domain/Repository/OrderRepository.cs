using Sales.Domain.Aggregates;
using Sales.Domain.Repository.Entity;

namespace Sales.Domain.Repository;


public interface IOrderRepository
{
    public Task CreateOrderWithCart(CartEntity cartEntity);
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

    public async Task UpdateOrder(OrderEntity orderEntity)
    {
        _salesDataContext.Update(orderEntity);
        await _salesDataContext.SaveChangesAsync();
    }
}