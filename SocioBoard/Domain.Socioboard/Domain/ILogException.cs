using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public interface ILogException
    {
        void AddLog(Log log);
        void DeleteLog(Log log);
        void UpdateLog(Log log);
        List<Log> getAllLogs(Log log);
    }
}