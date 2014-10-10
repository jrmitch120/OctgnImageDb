namespace OctgnImageDb.Logging
{
    public class LogManager
    {
        private static ILogFactory _logFactory;

        public static ILogFactory LogFactory
        {
            get
            {
                if (_logFactory == null)
                {
                    return new ConsoleLogFactory();
                }
                return _logFactory;
            }
            set { _logFactory = value; }
        }

        public static ILogger GetLogger()
        {
            return LogFactory.GetLogger();
        }
    }
}
