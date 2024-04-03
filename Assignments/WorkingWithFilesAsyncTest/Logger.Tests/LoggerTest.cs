using Moq;
using WorkingWithFilesAsync;

namespace WorkingWithFilesAsyncTest.Logger.Tests
{
    public class LoggerTest
    {
        private const string _loggerDirectory = "D:\\University materials\\Programmes\\internship-assignments\\Assignments\\WorkingWithFilesAsync\\";

        [Theory]
        [InlineData("Update", "success")]
        [InlineData("DeleteByID", "failure")]
        public async Task WriteLogToFile_ShouldWritetoFile(string methodName, string message)
        {
            var loggerMock = new Mock<ILogger>();
            var logger = loggerMock.Object;

            await logger.WriteLogToFile(methodName, message);
            loggerMock.Verify(l => l.WriteLogToFile(methodName, message), Times.Once);
        }

        [Theory]
        [InlineData("Add", "success")]
        [InlineData("GetByID", "failure")]
        public async Task WriteLogTofile_ShouldBeProperlyFormatted(string methodName, string message)
        {
            DateTimeOffset today = new(DateTime.UtcNow);
            string logFileName = $"Logs_{today.ToString("dd.MM.yyyy")}.txt";
            string logFilePath = _loggerDirectory + logFileName;

            if (File.Exists(logFilePath))
            {
                File.Delete(logFilePath);
            }

            var logger = new WorkingWithFilesAsync.Logger();
            await logger.WriteLogToFile(methodName, message);

            string[] lines = await File.ReadAllLinesAsync(logFilePath);
            Assert.Single(lines);

            string[] logParts = lines[0].Split("//");

            Assert.Multiple(() =>
            {
                Assert.Equal(3, logParts.Length);
                Assert.Equal(methodName, logParts[0]);
                Assert.Equal(message, logParts[2]);
            });
        }
    }
}
