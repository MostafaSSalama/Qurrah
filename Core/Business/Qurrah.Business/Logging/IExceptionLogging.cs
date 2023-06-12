namespace Qurrah.Business.Logging
{
    public interface IExceptionLogging
    {
        void Log(Exception exception);
        void Log(string errorMessage);
    }
}