using System;
using System.Collections.Generic;

namespace LovelyLogs
{
    public sealed class LogManager
    {
        private static List<Logger> loggers = new List<Logger>();
        private static readonly Lazy<LogManager> lazy =
            new Lazy<LogManager> (() => new LogManager());

        // This private constructor prevents LogManagers from being
        // instantiated by other classes
        private LogManager()
        {
        }

        public static Logger GetLogger(string loggerName)
        {
            if (!loggers.Exists(logger => logger.LoggerName == loggerName))
            {
                loggers.Add(new Logger(loggerName)); 
            }

            return loggers.Find(logger => logger.LoggerName == loggerName);
        }

        public static LogManager Instance
        {
            get
            {
                return lazy.Value;
            }
        }
    }
}
