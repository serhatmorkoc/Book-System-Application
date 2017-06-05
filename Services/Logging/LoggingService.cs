using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Data.Entities;

namespace Services.Logging
{
    public class LoggingService : ILoggingService
    {
        #region Fields

        private readonly IRepository<Log> _logRepository;

        #endregion

        #region Ctor

        public LoggingService(IRepository<Log> logRepository)
        {
            _logRepository = logRepository;
        }

        #endregion

        #region Methods

        public async Task<IEnumerable<Log>> GetLogsAsync(int pageSize = 10, int pageNumber = 1, string name = null)
        {
            var query = await _logRepository.TableAsync();

            //query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(item => item.Message.ToLower().Contains(name.ToLower()));

            query = query.OrderByDescending(x => x.Id);

            return query;
        }

        #endregion



    }
}
