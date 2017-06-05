using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extension.Response;
using Data.Models.Books;
using Data.Models.Logging;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Logging;

namespace Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/logs")]
    public class LogController : BaseController
    {
        #region Const

         

        #endregion

        #region Fields

        private readonly ILoggingService _loggingService;
        private readonly ILogger _logger;

        #endregion

        #region Ctor

        public LogController(ILoggingService loggingService, ILogger logger)
        {
            _loggingService = loggingService;
            _logger = logger;
        }

        #endregion

        #region Methods

        [Route("list")]
        public async Task<IActionResult> GetLogs(int? pageSize = 10, int? pageNumber = 1, string name = null)
        {
            var response = new ListModelResponse<LogListModel>() as IListModelResponse<LogListModel>;

            try
            {
                response.PageSize = (int)pageSize;
                response.PageNumber = (int)pageNumber;

                var query = await _loggingService.GetLogsAsync(response.PageSize, response.PageNumber, name);
                var result = query.Select(x => new LogListModel()
                {
                    Id = x.Id,
                    MessageTemplate = x.MessageTemplate,
                    TimeStamp = x.TimeStamp,
                    Message = x.Message,
                    Exception = x.Exception,
                    Level = x.Level,
                    LogEvent = x.LogEvent,
                    Properties = x.Properties,
                }).ToList();

                response.Model = result;
                response.Message = string.Format("Total of records: {0}", response.Model.Count());

                if (!response.Model.Any())
                {
                    _logger.Warning(MessageTemplate, ProtocolName(), RequestMethod(), TraceIdentifierName(), LoggingEvents.LIST_ITEMS_NOTFOUND, "LIST_ITEMS_NOTFOUND", RequestPath());

                    response.HasError = true;
                    response.ErrorMessage = "LIST_ITEMS_NOTFOUND";
                    response.ErrorCode = LoggingEvents.LIST_ITEMS_NOTFOUND;
                    return response.ToHttpResponse();
                }
            }
            catch (Exception ex)
            {
                _logger.Warning(MessageTemplate, ProtocolName(), RequestMethod(), TraceIdentifierName(), LoggingEvents.METHOD_ERROR, "METHOD_ERROR", RequestPath());

                response.HasError = true;
                response.ErrorMessage = ex.Message;
            }
            
            return response.ToHttpResponse();
        }

        #endregion
    }
}