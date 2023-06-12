namespace Qurrah.Business.Logging.ILogger
{
    public interface IErrorLogger
    {
        void Error(Exception exception);
        void Error(string errorMessage);
    }
}