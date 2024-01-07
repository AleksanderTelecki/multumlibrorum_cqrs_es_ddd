using CQRS.Core.Queries.Abstract;
using Product.Domain.Repository;
using Product.Messages.Events.Models;
using Product.Messages.Queries;

namespace Product.Domain.QueryHandlers;

public class BookQueryHandler:
    IQueryHandler<GetAllBooksQuery, List<BookModel>>
{

    private readonly IProductRepository _productRepository;
    
    public BookQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<List<BookModel>> Handle(GetAllBooksQuery query, CancellationToken cancellationToken)
    {
        var books = await _productRepository.GetAllBooks();

        return books
            .Select(x => new BookModel
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                Price = x.Price,
                PageCount = x.PageCount,
                Comments = x.Comments.Select(c => new Comment {Id = c.Id, UserName = c.UserName, RegDate = c.RegDate, UserId = c.UserId, CommentText = c.CommentText}).ToList(),
                PromotedPrice = x.PromotedPrice,
                Description = x.Description,
                RegDate = x.RegDate,
                IsHidden = x.IsHidden,
                Quantity = x.Quantity
            }).ToList();
    }
}