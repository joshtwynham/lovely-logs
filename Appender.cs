using System;

namespace LovelyLogs
{
    public abstract class Appender
    {
        public abstract void Append(string message);
    }
}
