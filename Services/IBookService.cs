using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksAsync(int pageSize = 10, int pageNumber = 1, string name = null,bool published = true);
        Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int authorId, int pageSize = 10, int pageNumber = 1, bool published = true);
        Task<IEnumerable<Book>> GetBooksByCategoryIdAsync(int categoryId, int pageSize = 10, int pageNumber = 1, string name = null, bool published = true);
        Task RentalInsertAsync(Rental rental);
        Task RentalUpdateAsync(Rental rental);
        Task<Book> GetBookByIdAsync(int bookId);
        Task InsertBookAsync(Book book);
        Task DeleteBookAsync(Book book);
        Task UpdateBookAsync(Book book);
    }
}
