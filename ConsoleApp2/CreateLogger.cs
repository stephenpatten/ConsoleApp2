using NLog.Fluent;

namespace ConsoleApp2
{
    public static class NLogBootstraper
    {
        //Creating Log messages

        //In order to create log messages from the application you need to use the logging API.There are two classes that you will be using the most: Logger and LogManager, both in the NLog namespace.Logger represents the named source of logs and has methods to emit log messages, and LogManager creates and manages instances of loggers.

        //It is important to understand that Logger does not represent any particular log output(and thus is never tied to a particular log file, etc.) but is only a source, which typically corresponds to a class in your code.Mapping from log sources to outputs is defined separately through Configuration File or Configuration API. Maintaining this separation lets you keep logging statements in your code and easily change how and where the logs are written, just by updating the configuration in one place.

        //public static Log.ILogger Init()
        //{


        //} 
    }
}