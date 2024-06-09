using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hospital.Presentation.Filters
{
    public class ModelValidationFilter : IActionFilter
    {
        private readonly ILogger<ModelValidationFilter> _logger;

        public ModelValidationFilter(ILogger<ModelValidationFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var requestPath = context.HttpContext.Request.Path;
            _logger.LogInformation("Path: {Path}\nModel validated successfully", requestPath);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var requestPath = context.HttpContext.Request.Path;

            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                                               .SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();

                _logger.LogError("Path: {Path}\nValidation errors occurred: {Errors}", requestPath, string.Join(", ", errors));

                context.Result = new BadRequestObjectResult( new { 
                    Message = "Model data for creation is invalid", 
                    Errors = errors 
                });
            }
        }
    }
}
