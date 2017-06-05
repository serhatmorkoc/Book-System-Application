using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Data.Entities;
using Data.Models.Authors;
using Data.Models.Books;
using Data.Models.Categories;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Rental> _rentalRepository;

        #region Ctor

        public BookService(IRepository<Book> bookRepository, IRepository<Rental> rentalRepository)
        {
            _bookRepository = bookRepository;
            _rentalRepository = rentalRepository;
        }

        #endregion

        #region Methods

        public async Task<IEnumerable<Book>> GetBooksAsync(int pageSize = 10, int pageNumber = 1, string name = null, bool published = true)
        {
            var query = await _bookRepository.TableAsync();

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            query = query.Where(x => x.Published == published);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(item => item.Name.ToLower().Contains(name.ToLower()));
            
            query = query.OrderBy(x => x.Name).ThenBy(x => x.CreatedDate);
            
            return query;
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int authorId, int pageSize = 10, int pageNumber = 1, bool published = true)
        {
            if (authorId == 0)
                return null;

            var query = await _bookRepository.TableAsync();

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            query = query.Where(x => x.Published == published);
            query = query.Where(x => x.AuthorId == authorId);
            query = query.OrderBy(x => x.Name).ThenBy(x => x.CreatedDate);

            return query;
        }

        public async Task<IEnumerable<Book>> GetBooksByCategoryIdAsync(int categoryId, int pageSize = 10, int pageNumber = 1, string name = null, bool published = true)
        {
            if (categoryId == 0)
                return null;

            var query = await _bookRepository.TableAsync();

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            query = query.Where(x => x.Published == published);
            query = query.Where(x => x.CategoryId == categoryId);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(item => item.Name.ToLower().Contains(name.ToLower()));

            query = query.OrderBy(x => x.Name).ThenBy(x => x.CreatedDate);

            return query;
        }

        public async Task RentalInsertAsync(Rental rental)
        {
            if (rental == null)
                throw new ArgumentNullException("rental");

            await _rentalRepository.InsertAsync(rental);
        }

        public async Task RentalUpdateAsync(Rental rental)
        {
            if (rental == null)
                throw new ArgumentNullException("rental");

            await _rentalRepository.UpdateAsync(rental);
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            if (bookId == 0)
                return null;

            var result = await _bookRepository.GetByIdAsync(bookId);
            return result;
        }

        public async Task InsertBookAsync(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("book");

            await _bookRepository.InsertAsync(book);
        }

        public async Task DeleteBookAsync(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("book");

            await _bookRepository.DeleteAsync(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("book");

            await _bookRepository.UpdateAsync(book);
        }

        #endregion





    }
}
