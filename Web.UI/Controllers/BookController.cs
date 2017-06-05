using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extension.Response;
using Data.Models.Books;
using Data.Models.Categories;
using Data.Models.Reserves;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace Web.UI.Controllers
{
    public class BookController : BaseController
    {
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        [HttpGet, Route("list")]
        public IActionResult List(int? pageSize = 10, int? pageNumber = 1, string searchText = null)
        {
            //api: books/list/
            var client = new RestClient(ApiBaseUrl + "books/list/");
            var req = new RestRequest
            {
                Method = Method.GET,
                Timeout = 5000,
                ReadWriteTimeout = 5000,

            };

            client.Authenticator = new HttpBasicAuthenticator(UserName, Password);
            var result = GetResponseListAsync<BookListModel>(client, req);

            return View(result);
        }

        [Authorize(Roles = "REGISTREDUSER")]
        [HttpGet, Route("previews/{bookName}-{bookId}")]
        public IActionResult Details(int bookId, string bookName)
        {
            // api: books/detail/id/
            var client = new RestClient(ApiBaseUrl + "books/detail/" + bookId);
            var req = new RestRequest
            {
                Method = Method.GET,
                Timeout = 5000,
                ReadWriteTimeout = 5000,

            };

            client.Authenticator = new HttpBasicAuthenticator(UserName, Password);
            var result = GetResponseSingleAsync<BookModel>(client, req);

            return View(result.Model);
        }

        [HttpPost, Route("/book/search/")]
        public IActionResult Search(int categoryId, string name)
        {
            //api: books/category/id/bookname/
            var client = new RestClient(ApiBaseUrl + "books/category/" + categoryId + "/" + name);
            var req = new RestRequest
            {
                Method = Method.GET,
                Timeout = 5000,
                ReadWriteTimeout = 5000,

            };

            client.Authenticator = new HttpBasicAuthenticator(UserName, Password);
            var result = GetResponseListAsync<BookListModel>(client, req);

            return View(result);
        }

        [HttpPost, Route("/book/reserve/")]
        public IActionResult Reserve(int bookId, string userEmail)
        {
            //api: books/reserve?userid=1&userEmail=1
            var client = new RestClient(ApiBaseUrl + "books/reserve?bookid=" + bookId + "&useremail=" + userEmail);
            var req = new RestRequest
            {
                Method = Method.GET,
                Timeout = 5000,
                ReadWriteTimeout = 5000,

            };

            client.Authenticator = new HttpBasicAuthenticator(UserName, Password);
            var result = GetResponseSingleAsync<ReserveResultModel>(client, req);

            if (result.HasError)
                ErrorNotification(result.ErrorMessage);
            else
                SuccessNotification(result.Model.Result);

            return RedirectToAction("List");
        }

        [HttpPost, Route("/book/refund/")]
        public IActionResult Refund(int bookId, string userEmail)
        {
            //TODO

            //api: books/reserve?userid=1&userEmail=1
            var client = new RestClient(ApiBaseUrl + "books/refund?bookid=" + bookId + "&useremail=" + userEmail);
            var req = new RestRequest
            {
                Method = Method.GET,
                Timeout = 5000,
                ReadWriteTimeout = 5000,

            };

            client.Authenticator = new HttpBasicAuthenticator(UserName, Password);
            var result = GetResponseSingleAsync<ReserveResultModel>(client, req);

            if (result.HasError)
                ErrorNotification(result.ErrorMessage);
            else
                SuccessNotification(result.Model.Result);

            return RedirectToAction("List");
        }

        public ActionResult HomepageCategories()
        {
            // api: books/category/list/
            var client = new RestClient(ApiBaseUrl + "books/category/list");
            var req = new RestRequest
            {
                Method = Method.GET,
                Timeout = 5000,
                ReadWriteTimeout = 5000,

            };

            client.Authenticator = new HttpBasicAuthenticator(UserName, Password);
            var result = GetResponseListAsync<CategoryModel>(client, req);

            ViewBag.AvailableCategories = new SelectList(result.Model, "Id", "Name");

            return View(result);


        }
    }
}