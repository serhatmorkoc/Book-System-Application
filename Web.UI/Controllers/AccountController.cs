using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Models.Account;
using Data.Models.Logging;
using Data.Models.Users;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Authenticators;

namespace Web.UI.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Login(LoginModel model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(string email, string password, string returnUrl)
        {

            //user/validateuser?email=test@domain.com&password=123456
            var client = new RestClient(ApiBaseUrl + "user/validateuser?email=" + email + "&password=" + password);
            var req = new RestRequest
            {
                Method = Method.GET,
                Timeout = 5000,
                ReadWriteTimeout = 5000,

            };

            client.Authenticator = new HttpBasicAuthenticator(UserName, Password);
            var result = GetResponseSingleAsync<UserValidateModel>(client, req);



            if (result.Model == UserValidateModel.UserNotExist)
                ErrorNotification("UserNotExist");

            if (result.Model == UserValidateModel.Deleted)
                ErrorNotification("User Deleted");

            if (result.Model == UserValidateModel.NotActive)
                ErrorNotification("User NotActive");

            if (result.Model == UserValidateModel.NotRegistered)
                ErrorNotification("User NotRegistered");

            if (result.Model == UserValidateModel.WrongPassword)
                ErrorNotification("User WrongPassword");

            if (result.Model == UserValidateModel.Successful)
            {
                var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, email), new Claim(ClaimTypes.Name, email) };

                var identity = new ClaimsIdentity(claims, "Forms");
                identity.AddClaim(new Claim(ClaimTypes.Role, "REGISTREDUSER"));
                var principal = new ClaimsPrincipal(identity);

                HttpContext.Authentication.SignInAsync("MyCookieMiddlewareInstance", principal);

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);

                //AddNotification("Success", "Login Success");
                return RedirectToAction("Index", "Book");
            }

            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Authentication.SignOutAsync("MyCookieMiddlewareInstance");
            return RedirectToAction("Index", "Book");
        }
    }
}