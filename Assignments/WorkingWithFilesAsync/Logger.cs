namespace WorkingWithFilesAsync
{
    internal class Logger
    {
        private static string _logDirectory = "D:\\University materials\\Programmes\\internship-assignments\\Assignments\\WorkingWithFilesAsync\\";

        public static async Task WriteLogToFile(string log)
        {
            using(StreamWriter sw = new StreamWriter(_logDirectory + "logs.txt", true))
            {
                await sw.WriteLineAsync(log);
            }
        }

        
    }
}
