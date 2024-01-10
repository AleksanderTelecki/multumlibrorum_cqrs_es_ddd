using Sales.Messages.Enums;

namespace Sales.Messages.Models;

public class OrderModel
{
    public Guid OrderId { get; set; }
    public Guid ClientId { get; set; }
    public List<OrderItemModel> Items { get; set; } = new List<OrderItemModel>();
    public OrderState State { get; set; }
}