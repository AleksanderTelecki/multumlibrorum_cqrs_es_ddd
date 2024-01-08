using Marte.EventSourcing.Core.Aggregate;
using Product.Messages.Events;
using Sales.Messages.Events;
using Sales.Messages.Models;

namespace Sales.Domain.Aggregates
{
    public class Cart: AggregateRoot
    {
        public Guid ClientId { get; set; }
        public List<CartItem> Items { get; set; }

        public Cart(Guid clientId)
        {
            Id = Guid.NewGuid();

            RaiseEvent(new CartCreatedEvent
            {
                Id = Id,
                ClientId = clientId
            });
        }

        public Cart()
        {
            
        }

        public void AddItem(Guid productId, int quantity)
        {
            var itemId = Guid.NewGuid();

            if (Items.Exists(x=> x.ProductId == productId))
            {
                var item = Items.Single(x => x.ProductId == productId);
                
                item.Quantity += quantity;
                quantity = item.Quantity;
                itemId = item.Id;
                
                RaiseEvent(new CartItemEditedEvent
                {
                    Id = Id,
                    ProductId = productId,
                    NewQuantity = quantity,
                    ItemId = itemId
                });
                
            }
            else
            {
                RaiseEvent(new CartItemAddedEvent
                {
                    Id = Id,
                    ItemId = itemId,
                    ProductId = productId,
                    Quantity = quantity
                });
            }
            
            
        }

        public void RemoveItem(Guid productId)
        {
            var itemId = Items.Single(x => x.ProductId == productId).Id;

            RaiseEvent(new CartItemRemovedEvent
            {
                Id = Id,
                ProductId = productId,
                ItemId = itemId
            });
        }

        public void EditItemQuantity(Guid productId, int newQuantity)
        {
            var itemId = Items.Single(x => x.ProductId == productId).Id;

            RaiseEvent(new CartItemEditedEvent
            {
                Id = Id,
                ProductId = productId,
                NewQuantity = newQuantity,
                ItemId = itemId
            });
        }

        public void Apply(CartItemEditedEvent @event)
        {
            var item = Items.Single(x => x.ProductId == @event.ProductId);
            item.Quantity = @event.NewQuantity;

            Version++;
        }

        public void Apply(CartItemRemovedEvent @event)
        {
            var item = Items.Single(x => x.ProductId == @event.ProductId);
            Items.Remove(item);

            Version++;
        }

        public void Apply(CartItemAddedEvent @event)
        {
            Items.Add(new CartItem(@event.ItemId, @event.ProductId, 1));

            Version++;
        }

        public void Apply(CartCreatedEvent @event)
        {
            Id = @event.Id;
            ClientId = @event.ClientId;
            Items = new List<CartItem>();

            Version++;
        }
    }
}
