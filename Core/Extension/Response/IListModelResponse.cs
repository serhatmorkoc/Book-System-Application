using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Extension.Response
{
    public interface IListModelResponse<T> : IResponse
    {
        int PageSize { get; set; }
        int PageNumber { get; set; }
        IEnumerable<T> Model { get; set; }
    }
}
