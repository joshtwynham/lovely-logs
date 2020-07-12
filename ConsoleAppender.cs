using System;

namespace LovelyLogs
{
    public class ConsoleAppender : Appender
    {
        public override void Append(string message)
        {
            Console.Write(message);
        }
    }
}
