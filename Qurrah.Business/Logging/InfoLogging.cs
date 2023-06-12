using Qurrah.Business.Logging.ILogger;

namespace Qurrah.Business.Logging
{
    public class InfoLogging : IInfoLogging
    {
        #region Fields
        IInfoLogger _logger;
        #endregion

        #region Ctor
        public InfoLogging(IInfoLogger logger)
        {
            _logger = logger;
        }
        #endregion

        #region Methods
        public void Info(string infoMessage)
        {
            _logger.Info(infoMessage);
        }
        #endregion
    }
}