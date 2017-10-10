using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{

    //In order to create log messages from the application you need to use the logging API.
    //There are two classes that you will be using the most: Logger and LogManager, 
    //both in the NLog namespace. Logger represents the named source of logs and has methods 
    //to emit log messages, and LogManager creates and manages instances of loggers.

    //It is important to understand that Logger does not represent any particular log output
    //(and thus is never tied to a particular log file, etc.) but is only a source, 
    //which typically corresponds to a class in your code. Mapping from log sources to outputs 
    //is defined separately through Configuration File or Configuration API. 
    //Maintaining this separation lets you keep logging statements in your code and 
    //easily change how and where the logs are written, just by updating the configuration in one place.

    class NLogAdapter : ILogger
    {
        public void Log(LogEntry entry)
        {
            NLog.Logger log;
            if (entry.Context != null)
            {
                log = NLog.LogManager.GetLogger(entry.Context.GetType().Namespace);
            }
            else
            {
                log = NLog.LogManager.GetLogger("DefaultLogger");
            }
            switch (entry.Severity)
            {
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
