namespace OctgnImageDb.Logging
{
    public interface ILogger
    {
        void Log(string message);
        void Log(string message, LogType logType);
    }
}