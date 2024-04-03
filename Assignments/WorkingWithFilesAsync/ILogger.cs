namespace WorkingWithFilesAsync
{
    public interface ILogger
    {
        Task WriteLogToFile(string methodName, string message);
    }
}
