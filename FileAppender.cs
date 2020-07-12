using System;

namespace LovelyLogs
{
    public class FileAppender : Appender
    {
        private string filePath = null;

        public FileAppender(string filePath)
        {
            this.filePath = filePath;
        }

        public override void Append(string message)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@filePath, true))
            {
                file.Write(message);
            }
        }
    }
}
