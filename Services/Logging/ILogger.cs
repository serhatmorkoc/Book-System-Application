using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain.Logging;

namespace Services.Logging
{
    public partial interface ILogger
    {
        bool IsEnabled(LogLevel level);
        void ClearLog();
        IList<Log> GetAllLogs(DateTime? fromUtc = null, DateTime? toUtc = null, string message = "", LogLevel? logLevel = null,int pageIndex = 0, int pageSize = int.MaxValue);
        Log GetLogById(int logId);
        IList<Log> GetLogByIds(int[] logIds);
        Log InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "");
        void DeleteLog(Log log);
        void DeleteLogs(IList<Log> logs);
    }
}
