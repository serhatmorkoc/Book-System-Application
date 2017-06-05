using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Extension.Response
{
    public interface  IResponse
    {
        string Message { get; set; }
        bool HasError { get; set; }
        string ErrorMessage { get; set; }
        int ErrorCode { get; set; }
    }
}
