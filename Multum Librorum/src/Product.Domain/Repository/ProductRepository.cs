using Microsoft.EntityFrameworkCore;
using Product.Domain.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Repository
{
    public interface IProductRepository
    {
        public Task CreateBook(BookEntity bookEntity);
        public Task DeleteBook(BookEntity bookEntity);
        public Task<BookEntity> GetBook(Guid id);
        public Task<List<BookEntity>> GetAllBooks();
        public Task UpdateBook(BookEntity bookEntity);

    }

    public class ProductRepository : IProductRepository
    {
        private readonly ProductDomainDataContext _productDomainDataContext;

        public ProductRepository(ProductDomainDataContext productDomainDataContext)
        {
            _productDomainDataContext = productDomainDataContext;
        }

        public async Task CreateBook(BookEntity bookEntity)
        {
            _productDomainDataContext.Books.Add(bookEntity);
            await _productDomainDataContext.SaveChangesAsync();
        }

        public async Task DeleteBook(BookEntity bookEntity)
        {
            _productDomainDataContext.Books.Remove(bookEntity);
            await _productDomainDataContext.SaveChangesAsync();
        }

        public async Task<List<BookEntity>> GetAllBooks()
        {
            return await _productDomainDataContext.Books.AsNoTracking().ToListAsync();
        }

        public async Task<BookEntity> GetBook(Guid id)
        {
            return await _productDomainDataContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateBook(BookEntity bookEntity)
        {
            _productDomainDataContext.Books.Update(bookEntity);
            await _productDomainDataContext.SaveChangesAsync();
        }
    }
}
