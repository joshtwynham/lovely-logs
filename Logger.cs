using System;
using System.Collections.Generic;

namespace LovelyLogs
{
    public class Logger : ILog
    {
        private List<Appender> appenders = null;
        private LogLevel logLevel = LogLevel.ALL;
        private string dateFormat = "yyyy-MM-dd H:mm:ss.ffff";
        private string logFormat = "%date{yyyy-MM-dd H:mm:ss.ffff} %logLevel %logger %message%newLine";

        public string LoggerName { get; } = null;

        public Logger(string loggerName)
        {
            LoggerName = loggerName;
            appenders = new List<Appender>
            {
                new ConsoleAppender()
            };
        }

        public void Debug(string message)
        {
            if (LogLevel.DEBUG >= logLevel)
                Record(FormatLog(message, "DEBUG"));
        }
        public void Info(string message)
        {
            if (LogLevel.INFO >= logLevel)
                Record(FormatLog(message, "INFO"));
        }
        public void Warn(string message)
        {
            if (LogLevel.WARN >= logLevel)
                Record(FormatLog(message, "WARN"));
        }
        public void Error(string message)
        {
            if (LogLevel.ERROR >= logLevel)
                Record(FormatLog(message, "ERROR"));
        }
        public void Fatal(string message)
        {
            if (LogLevel.FATAL >= logLevel)
                Record(FormatLog(message, "FATAL"));
        }

        public void AddAppender(Appender appender)
        {
            appenders.Add(appender);
        }

        public void RemoveAppender(Appender appender)
        {
            appenders.RemoveAll(x => x.GetType().Equals(appender.GetType()));
        }

        public void SetLogFormat(string format)
        {
            logFormat = format;
        }

        public void SetLogLevel(LogLevel level)
        {
            logLevel = level;
        }

        private void Record(string message)
        {
            foreach (Appender appender in appenders)
            {
                appender.Append(message);
            }
        }

        private string FormatLog(string message, string logLevel)
        {
            DateTime now = DateTime.Now;
            string[] formatElements = logFormat.Split('%');
            string log = null;
            
            for (int i = 0; i < formatElements.Length; i++)
            {
                if (formatElements[i].Contains("date"))
                {
                    // This checks to see if the date contains a custom format
                    if (formatElements[i].Contains('{'))
                    {
                        dateFormat = formatElements[i].Split(new char[] { '{', '}' })[1];
                    }

                    log += now.ToString(dateFormat);
                }
                else if (formatElements[i].Contains("logLevel"))
                {
                    log += logLevel;
                }
                else if (formatElements[i].Contains("logger"))
                {
                    log += LoggerName;
                }
                else if (formatElements[i].Contains("message"))
                {
                    log += message;
                }
                else if (formatElements[i].Contains("newLine"))
                {
                    log += '\n';
                }

                if (i > 0 && !formatElements[i].Contains("newLine"))
                {
                    log += ' ';
                }
            }
            return log;
        }
    }
}
