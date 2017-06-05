using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models.Books;
using Data.Models.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Authenticators;

namespace Web.UI.Controllers
{
    [Authorize(Roles = "REGISTREDUSER")]
    public class LogController : BaseController
    {
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult List(int? pageSize = 10, int? pageNumber = 1, string searchText = null)
        {
            //logs/list?name=q&pageSize=1&pageNumber=1
            var client = new RestClient(ApiBaseUrl + "logs/list?name=&pageSize=50&pageNumber=1");
            var req = new RestRequest
            {
                Method = Method.GET,
                Timeout = 5000,
                ReadWriteTimeout = 5000,

            };

            client.Authenticator = new HttpBasicAuthenticator(UserName, Password);
            var result = GetResponseListAsync<LogListModel>(client, req);

            return View(result);
        }
    }
}