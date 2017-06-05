using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ILogger _logger;
        const string MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

        public ValuesController(IBookService bookService, ILogger logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        [Route("Books")]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _bookService.GetBooksAsync();

            return Ok(result);
        }


        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
           
            var result = _bookService.GetBooksAsync();
            var q = result.Result;

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
