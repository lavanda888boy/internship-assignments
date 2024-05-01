namespace Hospital.Presentation.Middleware
{
    public class RequestTimingMiddleware
    {
        private readonly ILogger<RequestTimingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public RequestTimingMiddleware(ILogger<RequestTimingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestReceiveTime = DateTime.UtcNow;
            await _next.Invoke(context);
            _logger.LogInformation($"Request {context.Request.Method} {context.Request.Path} was processed" +
                $" in {DateTime.UtcNow - requestReceiveTime} ms");
        }
    }
}
