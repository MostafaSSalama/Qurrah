using Qurrah.Business.Logging.ILogger;
using Serilog;

namespace Qurrah.Business.Logging.Logger
{
    public class SeriLogger : IErrorLogger, IInfoLogger
    {
        public void Error(Exception exception)
        {
            Log.Error("An error occured {0}", exception.ToString());
        }
        public void Error(string errorMessage)
        {
            Log.Error(errorMessage);
        }
        public void Info(string infoMessage)
        {
            Log.Information(infoMessage);
        }
    }
}