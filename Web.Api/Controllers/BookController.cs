using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Extension.Response;
using Data.Entities;
using Data.Models.Authors;
using Data.Models.Books;
using Data.Models.Categories;
using Data.Models.Reserves;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services;
using Services.Logging;
using Services.Users;

namespace Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/books")]
    public class BookController : BaseController
    {
        #region Fields

        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly IAuthorService _authorService;
        private readonly ILogger _logger;

        #endregion

        #region Ctor

        public BookController(IBookService bookService, ILogger logger, ICategoryService categoryService, IAuthorService authorService, IUserService userService)
        {
            _bookService = bookService;
            _logger = logger;
            _categoryService = categoryService;
            _authorService = authorService;
            _userService = userService;
        }

        #endregion

        #region Methods

        [Route("list")]
        public async Task<IActionResult> GetBooks(int? pageSize = 25, int? pageNumber = 1, string name = null)
        {
            var response = new ListModelResponse<BookListModel>() as IListModelResponse<BookListModel>;
            try
            {
                response.PageSize = (int)pageSize;
                response.PageNumber = (int)pageNumber;

                var query = await _bookService.GetBooksAsync(response.PageSize, response.PageNumber, name);
                if (!query.Any())
                {
                    _logger.Warning(MessageTemplate, ProtocolName(), RequestMethod(), TraceIdentifierName(), LoggingEvents.LIST_ITEMS_NOTFOUND, "LIST_ITEMS_NOTFOUND", RequestPath());

                    response.HasError = true;
                    response.ErrorMessage = "LIST_ITEMS_NOTFOUND";
                    response.ErrorCode = LoggingEvents.LIST_ITEMS_NOTFOUND;
                    return response.ToHttpResponse();
                }

                var result = query.Select(x => new BookListModel()
                {
                    Isbn = x.Isbn,
                    Publisher = x.Publisher,
                    Stock = x.Stock,
                    Language = x.Language,
                    AuthorModel = new AuthorModel()
                    {
                        Id = _authorService.GetAuthorByIdAsync(x.AuthorId).Result.Id,
                        FirstName = _authorService.GetAuthorByIdAsync(x.AuthorId).Result.FirstName,
                        LastName = _authorService.GetAuthorByIdAsync(x.AuthorId).Result.LastName
                    },
                    CategoryModel = new CategoryModel()
                    {
                        Id = _categoryService.GetCategoryByIdAsync(x.CategoryId).Result.Id,
                        Name = _categoryService.GetCategoryByIdAsync(x.CategoryId).Result.Name,

                    },
                    Name = x.Name,
                    Published = x.Published,
                    CreatedDate = x.CreatedDate,
                    Id = x.Id,
                    Description = x.Description,
                    Hardcover = x.Hardcover,
                    ProductDimensions = x.ProductDimensions

                }).ToList();

                response.Model = result;
                response.Message = string.Format("Total of records: {0}", response.Model.Count());

            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.ErrorMessage = ex.Message;
            }

            _logger.Information(MessageTemplate, ProtocolName(), RequestMethod(), TraceIdentifierName(), LoggingEvents.LIST_ITEMS, "LIST_ITEMS", RequestPath(), RequestPathString());
            return response.ToHttpResponse();
        }

        [Route("detail/{bookId}")]
        public async Task<IActionResult> GetBookById(int bookId)
        {
            var response = new SingleModelResponse<BookModel>() as ISingleModelResponse<BookModel>;

            var query = await _bookService.GetBookByIdAsync(bookId);
            if (query == null)
            {
                _logger.Warning(MessageTemplate, ProtocolName(), RequestMethod(), TraceIdentifierName(), LoggingEvents.GET_ITEM_NOTFOUND, "GET_ITEM_NOTFOUND", RequestPath());

                response.HasError = true;
                response.ErrorMessage = "GET_ITEM_NOTFOUND";
                response.ErrorCode = LoggingEvents.GET_ITEM_NOTFOUND;
                return response.ToHttpResponse();
            }

            var result = new BookModel()
            {
                Id = query.Id,
                Isbn = query.Isbn,
                Name = query.Name,
                Publisher = query.Publisher,
                Stock = query.Stock,
                Language = query.Language,
                Hardcover = query.Hardcover,
                Description = query.Description,
                ProductDimensions = query.ProductDimensions
            };

            response.Model = result;
            response.Message = string.Format("Total of records: {0}", 1);

            _logger.Information(MessageTemplate, ProtocolName(), RequestMethod(), TraceIdentifierName(), LoggingEvents.GET_ITEM, "GET_ITEM", RequestPath());
            return response.ToHttpResponse();
        }

        [Route("category/{id}/{name}")]
        public async Task<IActionResult> GetBooksByCategoryId(int id, int? pageSize = 25, int? pageNumber = 1, string name = null)
        {
            var response = new ListModelResponse<BookListModel>() as IListModelResponse<BookListModel>;
            try
            {
                response.PageSize = (int)pageSize;
                response.PageNumber = (int)pageNumber;

                var query = await _bookService.GetBooksByCategoryIdAsync(id, response.PageSize, response.PageNumber, name);
                if (!query.Any())
                {
                    _logger.Warning(MessageTemplate, ProtocolName(), RequestMethod(), TraceIdentifierName(), LoggingEvents.LIST_ITEMS_NOTFOUND, "LIST_ITEMS_NOTFOUND", RequestPath());

                    response.HasError = true;
                    response.ErrorMessage = "LIST_ITEMS_NOTFOUND";
                    response.ErrorCode = LoggingEvents.LIST_ITEMS_NOTFOUND;
                    return response.ToHttpResponse();
                }

                var result = query.Select(x => new BookListModel()
                {
                    Isbn = x.Isbn,
                    Publisher = x.Publisher,
                    Language = x.Language,
                    Stock = x.Stock,
                    AuthorModel = new AuthorModel()
                    {
                        Id = _authorService.GetAuthorByIdAsync(x.AuthorId).Result.Id,
                        FirstName = _authorService.GetAuthorByIdAsync(x.AuthorId).Result.FirstName,
                        LastName = _authorService.GetAuthorByIdAsync(x.AuthorId).Result.LastName
                    },
                    CategoryModel = new CategoryModel()
                    {
                        Id = _categoryService.GetCategoryByIdAsync(x.CategoryId).Result.Id,
                        Name = _categoryService.GetCategoryByIdAsync(x.CategoryId).Result.Name,

                    },
                    Name = x.Name,
                    Published = x.Published,
                    CreatedDate = x.CreatedDate,
                    Id = x.Id,
                    Description = x.Description,
                    Hardcover = x.Hardcover,
                    ProductDimensions = x.ProductDimensions

                }).ToList();

                response.Model = result;
                response.Message = string.Format("Total of records: {0}", response.Model.Count());

            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.ErrorMessage = ex.Message;
            }

            _logger.Information(MessageTemplate, ProtocolName(), RequestMethod(), TraceIdentifierName(), LoggingEvents.LIST_ITEMS, "LIST_ITEMS", RequestPath(), RequestPathString());
            return response.ToHttpResponse();
        }

        [Route("reserve")]
        public async Task<IActionResult> BookReserve(int bookId, string userEmail)
        {
            var response = new SingleModelResponse<ReserveResultModel>() as ISingleModelResponse<ReserveResultModel>;
            try
            {
                var book = await _bookService.GetBookByIdAsync(bookId);
                var user = await _userService.GetUserByEmailAsync(userEmail);
                if (book.Stock != 0)
                {
                    book.Stock = book.Stock - 1;
                    await _bookService.UpdateBookAsync(book);

                    var rental = new Rental()
                    {
                        BookId = bookId,
                        UserId = user.Id,
                        ReserveDate = DateTime.Now,
                        ReturnDate = DateTime.Now.AddDays(7),
                        Returned = false
                    };
                    await _bookService.RentalInsertAsync(rental);

                    response.HasError = false;
                    response.Model = new ReserveResultModel() { Result = "Successful for " + user.FirstName + " " + user.LastName };

                    _logger.Information(MessageTemplate, ProtocolName(), RequestMethod(), TraceIdentifierName(), LoggingEvents.GET_ITEM, "RESERVE SUCCESSFUL", RequestPath(), RequestPathString());
                    return response.ToHttpResponse();

                }

                response.HasError = true;
                response.Model = new ReserveResultModel() { Result = "Unsuccessful" };
                response.ErrorMessage = "No stock for this book";

                _logger.Warning(MessageTemplate, ProtocolName(), RequestMethod(), TraceIdentifierName(), LoggingEvents.GET_ITEM_NOTFOUND, "NO STOCK FOR THIS BOOK", RequestPath(), RequestPathString());
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.ToHttpResponse();
        }

        [Route("refund")]
        public async Task<IActionResult> BookRefund(int bookId, string userEmail)
        {
            var response = new SingleModelResponse<ReserveResultModel>() as ISingleModelResponse<ReserveResultModel>;
            try
            {
                var book = await _bookService.GetBookByIdAsync(bookId);
                var user = await _userService.GetUserByEmailAsync(userEmail);

                 //TODO

                response.HasError = true;
                response.Model = new ReserveResultModel() { Result = "Unsuccessful" };
                response.ErrorMessage = "No stock for this book";

                _logger.Warning(MessageTemplate, ProtocolName(), RequestMethod(), TraceIdentifierName(), LoggingEvents.GET_ITEM_NOTFOUND, "BOOK NOT RETURNED", RequestPath(), RequestPathString());
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.ToHttpResponse();
        }

        [Route("category/list")]
        public async Task<IActionResult> GetCategories()
        {
            var response = new ListModelResponse<CategoryModel>() as IListModelResponse<CategoryModel>;

            var query = await _categoryService.GetCategoriesAsync();
            if (query == null || !query.Any())
            {
                _logger.Warning(MessageTemplate, ProtocolName(), RequestMethod(), TraceIdentifierName(), LoggingEvents.LIST_ITEMS_NOTFOUND, "LIST_ITEMS_NOTFOUND", RequestPath());

                response.HasError = true;
                response.ErrorMessage = "LIST_ITEMS_NOTFOUND";
                response.ErrorCode = LoggingEvents.LIST_ITEMS_NOTFOUND;
                return response.ToHttpResponse();
            }

            var result = query.Select(x => new CategoryModel()
            {
                Id = x.Id,
                Name = x.Name
            });

            response.Model = result;
            response.Message = string.Format("Total of records: {0}", response.Model.Count());

            _logger.Information(MessageTemplate, ProtocolName(), RequestMethod(), TraceIdentifierName(), LoggingEvents.LIST_ITEMS, "LIST_ITEMS", RequestPath());
            return response.ToHttpResponse();
        }

        [Route("author/{authorid}")]
        public async Task<IActionResult> GetBooksByAuthorId(int authorId)
        {
            var response = new ListModelResponse<Book>() as IListModelResponse<Book>;

            var result = await _bookService.GetBooksByAuthorIdAsync(authorId);
            if (result == null || !result.Any())
            {
                _logger.Warning(MessageTemplate, ProtocolName(), RequestMethod(), TraceIdentifierName(), LoggingEvents.LIST_ITEMS_NOTFOUND, "LIST_ITEMS_NOTFOUND", RequestPath());

                response.HasError = true;
                response.ErrorMessage = "LIST_ITEMS_NOTFOUND";
                response.ErrorCode = LoggingEvents.LIST_ITEMS_NOTFOUND;
                return response.ToHttpResponse();
            }

            response.Model = result;
            response.Message = string.Format("Total of records: {0}", response.Model.Count());

            _logger.Information(MessageTemplate, ProtocolName(), RequestMethod(), TraceIdentifierName(), LoggingEvents.LIST_ITEMS, "LIST_ITEMS", RequestPath());
            return response.ToHttpResponse();
        }

        #endregion


    }
}