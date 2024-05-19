using Hospital.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Presentation.Middleware
{
    public class DbTransactionMiddleware
    {
        private readonly ILogger<DbTransactionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public DbTransactionMiddleware(ILogger<DbTransactionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, HospitalManagementDbContext dbContext)
        {
            var pathValue = httpContext.Request.Path.Value;

            if (!string.IsNullOrEmpty(pathValue))
            {
                bool isGetRequest = httpContext.Request.Method == HttpMethod.Get.Method;
                bool isPostRequestWithSearchOrLogin = httpContext.Request.Method == HttpMethod.Post.Method &&
                    (pathValue.Contains("Search") || pathValue.Contains("Login"));

                bool requestNeedsTransactionCreation = isGetRequest || isPostRequestWithSearchOrLogin;

                if (requestNeedsTransactionCreation)
                {
                    _logger.LogInformation("Skipping transaction initialization: {Path}", pathValue);
                    await _next(httpContext);
                    return;
                }

                using var transaction = await dbContext.Database.BeginTransactionAsync(
                    System.Data.IsolationLevel.RepeatableRead
                );

                try
                {
                    _logger.LogInformation("Initializing transaction: {Path}", pathValue);
                    await _next(httpContext);
                    await dbContext.Database.CommitTransactionAsync();
                    _logger.LogInformation("Transaction succesfully committed: {Path}", pathValue);
                }
                catch (Exception ex)
                {
                    _logger.LogError("An error ocuured while generating database transaction: {Error}", ex.Message);
                    await dbContext.Database.RollbackTransactionAsync();
                    throw;
                }
            }
            else
            {
                await _next(httpContext);
            }
        }
    }
}
