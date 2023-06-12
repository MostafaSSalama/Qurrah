using Qurrah.Business.Logging.ILogger;

namespace Qurrah.Business.Logging
{
    public class ExceptionLogging : IExceptionLogging
    {
        #region Fields
        IErrorLogger _logger;
        #endregion

        #region Ctor
        public ExceptionLogging(IErrorLogger logger)
        {
            _logger = logger;
        }
        #endregion

        #region Methods
        public void Log(Exception exception)
        {
            _logger.Error(exception);
        }
        public void Log(string errorMessage)
        {
            _logger.Error(errorMessage);
        }
        #endregion
    }
}