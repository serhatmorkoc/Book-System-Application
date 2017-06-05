using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Extension.Response
{
    public interface ISingleModelResponse<T> : IResponse
    {
        T Model { get; set; }
    }
}
