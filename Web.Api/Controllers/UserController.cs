using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extension.Response;
using Data.Models.Books;
using Data.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Logging;
using Services.Users;

namespace Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        #region Ctor

        public UserController(ILogger logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        #endregion

        #region Methods

        [Route("validateuser")]
        public async Task<IActionResult> ValidateUser(string email, string password)
        {
            var response = new SingleModelResponse<UserValidateModel>() as ISingleModelResponse<UserValidateModel>;

            try
            {
                var result = await _userService.ValidateUserAsync(email, password);

                if (result == UserValidateModel.Successful)
                    response.Model = result;

                if (result == UserValidateModel.UserNotExist)
                    response.Model = result;

                if (result == UserValidateModel.Deleted)
                    response.Model = result;

                if (result == UserValidateModel.NotActive)
                    response.Model = result;

                if (result == UserValidateModel.NotRegistered)
                    response.Model = result;

                if (result == UserValidateModel.WrongPassword)
                    response.Model = result;

                response.Message = string.Format("Result of user: {0}", response.Model.ToString());

            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.ErrorMessage = ex.Message;
            }

            _logger.Information(MessageTemplate, ProtocolName(), RequestMethod(), TraceIdentifierName(), LoggingEvents.GET_ITEM, "GET_ITEM", RequestPath());
            return response.ToHttpResponse();
        }

        #endregion



    }
}