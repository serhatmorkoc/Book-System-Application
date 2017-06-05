using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain.Logging;
using Data.Entities;

namespace Services.Logging
{
   public  class Logger : ILogger
    {
        public bool IsEnabled(LogLevel level)
        {
            throw new NotImplementedException();
        }

        public void ClearLog()
        {
            throw new NotImplementedException();
        }

        public IList<Log> GetAllLogs(DateTime? fromUtc = null, DateTime? toUtc = null, string message = "", LogLevel? logLevel = null,
            int pageIndex = 0, int pageSize = Int32.MaxValue)
        {
            throw new NotImplementedException();
        }

        public Log GetLogById(int logId)
        {
            throw new NotImplementedException();
        }

        public IList<Log> GetLogByIds(int[] logIds)
        {
            throw new NotImplementedException();
        }

        public Log InsertLog(LogLevel logLevel, string message, string messageTemplate = "")
        {
            throw new NotImplementedException();
        }

        public void DeleteLog(Log log)
        {
            throw new NotImplementedException();
        }

        public void DeleteLogs(IList<Log> logs)
        {
            throw new NotImplementedException();
        }
    }
}
