using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class NLogAdapter<T> : ILogger
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetLogger((typeof(T).FullName));

        public void Log(LogEntry entry)
        {
            switch (entry.Severity)
            {
                case LoggingEventType.Verbose:
                    log.Trace(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Debug:
                    log.Debug(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Information:
                    log.Info(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Warning:
                    log.Warn(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Error:
                    log.Error(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Fatal:
                    log.Fatal(entry.Exception, entry.Message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(entry));
            }
        }
    }
}
