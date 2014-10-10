namespace OctgnImageDb.Logging
{
    public class ConsoleLogFactory : ILogFactory
    {
        public ILogger GetLogger()
        {
            return (new ConsoleLogger());
        }
    }
}