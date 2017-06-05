using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Services.Logging
{
    public interface ILoggingService
    {
        Task<IEnumerable<Log>> GetLogsAsync(int pageSize = 10, int pageNumber = 1, string name = null);
    }
}
