using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Extension.Response
{
    public class SingleModelResponse<T> : ISingleModelResponse<T>
    {

        public T Model { get; set; }
        public string Message { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
    }
}
