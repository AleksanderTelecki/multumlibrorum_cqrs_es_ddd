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
    IQueryHandler<GetClientCartIdQuery, Guid>
{
    private readonly ICartRepository _cartRepository;
    private readonly IRestDispatcher _restDispatcher;

    public SalesQueryHandler(ICartRepository cartRepository, IRestDispatcher restDispatcher)
    {
        _cartRepository = cartRepository;
        _restDispatcher = restDispatcher;
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
}