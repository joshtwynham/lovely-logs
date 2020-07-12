using System;

namespace LovelyLogs
{
    public class RollingFileAppender : Appender
    {
        private string directory = null;

        public RollingFileAppender(string directory)
        {
            this.directory = directory;
        }

        public override void Append(string message)
        {
            string logFile = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            string path = directory + logFile;

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@path, true))
            {
                file.Write(message);
            }
        }
    }
}
