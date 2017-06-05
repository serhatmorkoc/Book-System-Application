using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    public class BaseController : Controller
    {
        #region Const

        public const string MessageTemplate = "{Protocol} [{RequestMethod}] {TraceIdentifier} {EventCode} {EventName} {RequestPath}";

        #endregion

        public string ProtocolName()
        {
            return ControllerContext.HttpContext.Request.Protocol;
        }

        public string RequestMethod()
        {
            return ControllerContext.HttpContext.Request.Method;
        }

        public string TraceIdentifierName()
        {
            return ControllerContext.HttpContext.TraceIdentifier;
        }

        public string RequestPath()
        {
            return ControllerContext.HttpContext.Request.Path;
        }

        public string RequestPathString()
        {
            return ControllerContext.HttpContext.Request.QueryString.Value;

        }

        

        


    }
}