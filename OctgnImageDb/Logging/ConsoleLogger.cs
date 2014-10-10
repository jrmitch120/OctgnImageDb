using System;

namespace OctgnImageDb.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Log(message, LogType.Generic);
        }

        public void Log(string message, LogType logType)
        {
            Console.WriteLine(MessageFormatter.FormatConsoleMessage(message, logType));
        }
    }
}