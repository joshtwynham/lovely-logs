using System;
namespace LovelyLogs
{
    public interface ILog
    {
        public void Debug(string message);
        public void Info(string message);
        public void Warn(string message);
        public void Error(string message);
        public void Fatal(string message);

        public void AddAppender(Appender appender);
        public void RemoveAppender(Appender appender);

        public void SetLogFormat(string format);
        public void SetLogLevel(LogLevel level);
    }
}
