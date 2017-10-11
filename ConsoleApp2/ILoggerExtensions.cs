using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public static class ILoggerExtensions
    {
        public static void Log(this ILogger logger, string message)
        {
            logger.Log(new LogEntry(LoggingEventType.Information, message));
        }

        public static void Log(this ILogger logger, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Error, exception.Message, exception));
        }

        // More methods here.
    }

    /// <summary>
    /// Use MonitoredScope within a using statement to measure the time the operation needs.
    /// The result will be written to the NLog logger.
    /// </summary>
    public class MonitoredScope : IDisposable
    {
        readonly ILogger _logger;
        readonly object _identifier;
        readonly Stopwatch _watch;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public MonitoredScope(ILogger logger, object identifier)
        {
            _logger = logger;
            _identifier = identifier;
            _logger.Log(new LogEntry(LoggingEventType.Information, $"Beginning operation {_identifier}"));
            _watch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            _watch.Stop();
            _logger.Log(new LogEntry(LoggingEventType.Information, $"Completed operation {_identifier} ({_watch.ElapsedMilliseconds} ms)"));
        }
    }
}
