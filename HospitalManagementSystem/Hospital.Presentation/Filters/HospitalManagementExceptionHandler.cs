using Hospital.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.Common;

namespace Hospital.Presentation.Filters
{
    public class HospitalManagementExceptionHandler : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception is DbException)
            {
                context.Result = new ObjectResult($"Error in the database:\n{exception.Message}")
                {
                    StatusCode = 500
                };
            }
            else if (exception is NoEntityFoundException)
            {
                context.Result = new NotFoundObjectResult(exception.Message);
            }
            else
            {
                context.Result = new ObjectResult(exception.Message)
                {
                    StatusCode = 500
                };
            }

            context.ExceptionHandled = true;

            return Task.CompletedTask;
        }
    }
}
