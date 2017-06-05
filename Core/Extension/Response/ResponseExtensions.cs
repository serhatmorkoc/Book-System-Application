using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Core.Extension.Response
{
    public static class ResponseExtensions
    {
        public static IActionResult ToHttpResponse<T>(this IListModelResponse<T> response)
        {
            var status = HttpStatusCode.OK;

            if (response.HasError)
            {
                status = HttpStatusCode.InternalServerError;
            }
            else if (response.Model == null)
            {
                status = HttpStatusCode.NoContent;
            }

            return new ObjectResult(response) { StatusCode = (int)status };
        }

        public static IActionResult ToHttpResponse<T>(this ISingleModelResponse<T> response)
        {
            var status = HttpStatusCode.OK;

            if (response.HasError)
            {
                status = HttpStatusCode.InternalServerError;
            }
            else if (response.Model == null)
            {
                status = HttpStatusCode.NotFound;
            }

            return new ObjectResult(response) { StatusCode = (int)status };
        }
    }
}
