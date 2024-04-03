namespace WorkingWithFilesAsync
{
    public class Logger : ILogger
    {
        private string _logDirectory = "D:\\University materials\\Programmes\\internship-assignments\\Assignments\\WorkingWithFilesAsync\\";

        public Logger() { }

        public async Task WriteLogToFile(string methodName, string message)
        {
            DateTimeOffset today = new(DateTime.UtcNow);
            string logFileName = $"Logs_{today.ToString("dd.MM.yyyy")}.txt";

            using (StreamWriter sw = new StreamWriter(_logDirectory + logFileName, true))
            {
                await sw.WriteLineAsync($"{methodName}//{DateTime.UtcNow}//{message}");
            }
        }
    }
}
