using System;
using System.Collections.Generic;

namespace OctgnImageDb.Logging
{
    public class MessageFormatter
    {
        private static readonly Dictionary<LogType, string> LogLevelMap = new Dictionary<LogType, string>
        {
            {LogType.Generic, "{0}"},
            {LogType.Game, "{0}"},
            {LogType.Set, "-- {0}"},
            {LogType.Card,"---- {0}"},
            {LogType.Error, "**ERROR** {0}"},
        };

        public static string FormatConsoleMessage(string message, LogType logType)
        {
            return (String.Format(LogLevelMap[logType], message));
        }
    }
}