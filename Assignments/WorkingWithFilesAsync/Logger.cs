namespace WorkingWithFilesAsync
{
    internal class Logger
    {
        private static string _logDirectory = "D:\\University materials\\Programmes\\internship-assignments\\Assignments\\WorkingWithFilesAsync\\";

        public static async Task WriteLogToFile(string log)
        {
            DateTimeOffset today = new(DateTime.UtcNow);
            string logFileName = $"Logs_{today.ToString("dd.MM.yyyy")}.txt";
            using(StreamWriter sw = new StreamWriter(_logDirectory + logFileName, true))
            {
                await sw.WriteLineAsync(log);
            }
        }

        
    }
}
