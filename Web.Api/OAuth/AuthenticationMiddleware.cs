using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.WebEncoders.Testing;
using Serilog;
using Serilog.Events;

namespace Web.Api.OAuth
{

    public class AuthenticationMiddleware : System.Attribute
    {
        #region Const

        const string MessageTemplateInfo = "{Protocol} {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
        const string MessageTemplateErr = "{Protocol} {RequestMethod} {RequestPath} responded {StatusCode} Error: {Exception}";

        #endregion

        #region Fields

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        #endregion

        #region Ctor

        public AuthenticationMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        #endregion

        #region Methods

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                //_logger.Information(await FormatRequest(context.Request));
                //var originalBodyStream = context.Response.Body;

                //using (var responseBody = new MemoryStream())
                //{
                //    context.Response.Body = responseBody;

                //    await _next(context);

                //    _logger.Information(await FormatResponse(context.Response));
                //    await responseBody.CopyToAsync(originalBodyStream);

                //}

                var encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                var encoding = Encoding.GetEncoding("iso-8859-1");
                var usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                var seperatorIndex = usernamePassword.IndexOf(':');

                var username = usernamePassword.Substring(0, seperatorIndex);
                var password = usernamePassword.Substring(seperatorIndex + 1);

                if (username == "U1" && password == "P1")
                {
                    var claims = new[] { new Claim("name", username), new Claim(ClaimTypes.Role, "Role.Standart") };
                    var identity = new ClaimsIdentity(claims, "Basic");
                    context.User = new ClaimsPrincipal(identity);

                    var sw = Stopwatch.StartNew();
                    try
                    {
                        _logger.Information(MessageTemplateInfo, context.Request.Protocol, context.Request.Method, context.Request.Path, context.Response.StatusCode, sw.Elapsed.TotalMilliseconds);
                        await _next.Invoke(context);
                        sw.Stop();
                    }
                    catch (Exception ex)
                    {
                        context.Response.StatusCode = 401;
                        _logger.Error(MessageTemplateErr, context.Request.Protocol, context.Request.Method, context.Request.Path, context.Response.StatusCode, ex.Message);
                        await context.Response.WriteAsync("The remote server returned an error: (401) Unauthorized.");
                    }
                }
                else
                {
                    context.Response.StatusCode = 401;
                    _logger.Error(MessageTemplateErr, context.Request.Protocol, context.Request.Method, context.Request.Path, context.Response.StatusCode, "Unauthorized.");
                    await context.Response.WriteAsync("The remote server returned an error: (401) Unauthorized.");
                }
            }
            else
            {

                context.Response.StatusCode = 401;
                _logger.Error(MessageTemplateErr, context.Request.Protocol, context.Request.Method, context.Request.Path, context.Response.StatusCode, "Unauthorized. No authorization header.");
                await context.Response.WriteAsync("The remote server returned an error: (401) Unauthorized. No authorization header.");
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;
            request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"Response {text}";
        }

        #endregion
    }
}
