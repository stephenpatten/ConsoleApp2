using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class NLogAdapter : ILogger
    {
        private readonly NLog.ILogger _logger;

        public NLogAdapter(NLog.ILogger logger)
        {
            _logger = logger;
        }

        public void Log(LogEntry entry)
        {

            if (entry.Severity == LoggingEventType.Debug)
            {
                _logger.Debug(entry.Exception, entry.Message);
            }
            else if (entry.Severity == LoggingEventType.Information)
            {
                _logger.Info(entry.Exception, entry.Message);
            }
            else if (entry.Severity == LoggingEventType.Warning)
            {
                _logger.Warn(entry.Exception, entry.Message);
            }
            else if (entry.Severity == LoggingEventType.Error)
            {
                _logger.Error(entry.Exception, entry.Message);
            }
            else
            {
                _logger.Fatal(entry.Exception, entry.Message);
            }
        }
    }
}
