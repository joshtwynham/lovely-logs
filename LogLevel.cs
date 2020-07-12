using System;

namespace LovelyLogs
{
    public enum LogLevel
    {
        ALL = 0,
        DEBUG = 10000,
        INFO = 20000,
        WARN = 30000,
        ERROR = 40000,
        FATAL = 50000,
        OFF = 60000
    }
}
