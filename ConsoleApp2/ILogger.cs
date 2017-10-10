﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    //https://stackoverflow.com/questions/5646820/logger-wrapper-best-practice
    //https://stackoverflow.com/questions/9892137/windsor-pulling-transient-objects-from-the-container
    //https://messagetemplates.org/

    public interface ILogger
    {
        void Log(LogEntry entry);
    }

    public enum LoggingEventType { Debug, Information, Warning, Error, Fatal };

    public class LogEntry
    {
        public readonly LoggingEventType Severity;
        public readonly string Message;
        public readonly Exception Exception;
        public readonly Type Context;

        public LogEntry(LoggingEventType severity, 
            string message, 
            Exception exception = null, 
            Type context = null)
        {
            if (message == null) throw new ArgumentNullException("message");
            if (message == string.Empty) throw new ArgumentException("empty", "message");

            this.Severity = severity;
            this.Message = message;
            this.Exception = exception;
            this.Context = context;
        }
    }
}
