using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hospital.Presentation.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var requestPath = context.HttpContext.Request.Path;

            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                                               .SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();

                //_logger.LogError($"Path: {requestPath}\nValidation errors occurred: {errors}", string.Join(", ", errors));

                context.Result = new BadRequestObjectResult( new { 
                    Message = "Model data for creation is invalid", 
                    Errors = errors 
                });
            }
        }
    }
}
