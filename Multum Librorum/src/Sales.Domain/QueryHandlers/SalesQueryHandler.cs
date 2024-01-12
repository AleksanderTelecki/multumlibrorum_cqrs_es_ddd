using CQRS.Communication.Abstract;
using CQRS.Communication.Enums;
using CQRS.Core.Queries.Abstract;
using Product.Messages.Queries;
using Sales.Domain.Repository;
using Sales.Messages.Models;
using Sales.Messages.Queries;

namespace Sales.Domain.QueryHandlers;

public class SalesQueryHandler: 
    IQueryHandler<GetClientCartQuery, List<CartItemModel>>,
    IQueryHandler<GetClientCartIdQuery, Guid>,
    IQueryHandler<GetClientOrdersModelQuery, List<OrderModel>>,
    IQueryHandler<GetOrdersQuery, List<OrderModel>>,
    IQueryHandler<GetOrderDetailsByOrderIdQuery, OrderModel>
{
    private readonly ICartRepository _cartRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IRestDispatcher _restDispatcher;

    public SalesQueryHandler(ICartRepository cartRepository, IRestDispatcher restDispatcher, IOrderRepository orderRepository)
    {
        _cartRepository = cartRepository;
        _restDispatcher = restDispatcher;
        _orderRepository = orderRepository;
    }
    
    public async Task<List<CartItemModel>> Handle(GetClientCartQuery query, CancellationToken cancellationToken)
    {
        var clientCart = await _cartRepository.GetCartByClientId(query.ClientId);
        List<CartItemModel> clientCartModel = new List<CartItemModel>();
        
        
        foreach (var cartItem in clientCart.Items)
        {
            var productInfo =
                await _restDispatcher.DispatchQuery(new GetBookModelQuery() { ProductId = cartItem.ProductId },
                    EndpointEnum.ProductEndpoint);

            if (productInfo.IsHidden || productInfo.Quantity < 1)
            {
                clientCartModel.Add(new CartItemModel(cartItem.Id, productInfo.Id, productInfo.Title,
                    (productInfo.PromotedPrice ?? productInfo.Price) * cartItem.Quantity, cartItem.Quantity, false)); 
            }
            else if(productInfo != null)
            {
                clientCartModel.Add(new CartItemModel(cartItem.Id, productInfo.Id, productInfo.Title,
                    (productInfo.PromotedPrice ?? productInfo.Price) * cartItem.Quantity, cartItem.Quantity, true));
            }
        }

        return clientCartModel;
    }
    
    public async Task<Guid> Handle(GetClientCartIdQuery query, CancellationToken cancellationToken)
    {
        var clientCart = await _cartRepository.GetCartByClientId(query.ClientId);
        return clientCart.Id;
    }

    public async Task<List<OrderModel>> Handle(GetClientOrdersModelQuery query, CancellationToken cancellationToken)
    {
        var clietOrders = await _orderRepository.GetOrdersByClientId(query.ClientId);
        List<OrderModel> orderModels = new List<OrderModel>();
        
        foreach (var order in clietOrders)
        {
            orderModels.Add(new OrderModel() { OrderId = order.Id, ClientId = order.ClientId, State = order.State});

            var orderModel = orderModels.Single(x => x.OrderId == order.Id);
            
            foreach (var orderItem in order.Items)
            {
                var productInfo =
                    await _restDispatcher.DispatchQuery(new GetBookModelQuery() { ProductId = orderItem.ProductId },
                        EndpointEnum.ProductEndpoint);

                orderModel.Items.Add(new OrderItemModel()
                {
                    Id = orderItem.Id, 
                    ProductId = productInfo.Id, 
                    ProductName = productInfo.Title,
                    ProductPrice = (productInfo.PromotedPrice ?? productInfo.Price) * orderItem.Quantity,
                    Quantity = orderItem.Quantity
                });
            }
            
            
        }

        return orderModels;
    }

    public async Task<List<OrderModel>> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetOrders();
        List<OrderModel> orderModels = new List<OrderModel>();
        
        foreach (var order in orders)
        {
            orderModels.Add(new OrderModel() { OrderId = order.Id, ClientId = order.ClientId, State = order.State});

            var orderModel = orderModels.Single(x => x.OrderId == order.Id);
            
            foreach (var orderItem in order.Items)
            {
                var productInfo =
                    await _restDispatcher.DispatchQuery(new GetBookModelQuery() { ProductId = orderItem.ProductId },
                        EndpointEnum.ProductEndpoint);

                orderModel.Items.Add(new OrderItemModel()
                {
                    Id = orderItem.Id, 
                    ProductId = productInfo.Id, 
                    ProductName = productInfo.Title,
                    ProductPrice = (productInfo.PromotedPrice ?? productInfo.Price) * orderItem.Quantity,
                    Quantity = orderItem.Quantity
                });
            }
            
            
        }

        return orderModels;
    }

    public async Task<OrderModel> Handle(GetOrderDetailsByOrderIdQuery query, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrdersByOrderId(query.OrderId);
        var orderModel = new OrderModel() { OrderId = order.Id, ClientId = order.ClientId, State = order.State };
            
        foreach (var orderItem in order.Items)
        {
            var productInfo =
                await _restDispatcher.DispatchQuery(new GetBookModelQuery() { ProductId = orderItem.ProductId },
                    EndpointEnum.ProductEndpoint);

            orderModel.Items.Add(new OrderItemModel()
            {
                Id = orderItem.Id,
                ProductId = productInfo.Id,
                ProductName = productInfo.Title,
                ProductPrice = (productInfo.PromotedPrice ?? productInfo.Price) * orderItem.Quantity,
                Quantity = orderItem.Quantity
            });
        }

        return orderModel;
    }
}