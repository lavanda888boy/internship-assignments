using Hospital.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.Common;

namespace Hospital.Presentation.Filters
{
    public class HospitalManagementExceptionHandler : ExceptionFilterAttribute
    {
        private readonly ILogger<HospitalManagementExceptionHandler> _logger;

        public HospitalManagementExceptionHandler(ILogger<HospitalManagementExceptionHandler> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var requestPath = context.HttpContext.Request.Path;

            if (exception is DbException)
            {
                _logger.LogError("Path: {Path}\nError in the database: {Message}", requestPath, exception.Message);
                context.Result = new ObjectResult($"Error in the database:\n{exception.Message}")
                {
                    StatusCode = 500
                };
            }
            else if (exception is UserRegistrationException)
            {
                _logger.LogError("Path: {Path}\nRegistration failed: {Message}", requestPath, exception.Message);
                context.Result = new ObjectResult(exception.Message)
                {
                    StatusCode = 500
                };
            }
            else if (exception is UserLoginException)
            {
                _logger.LogError("Path: {Path}\nLogin failed: {Message}", requestPath, exception.Message);
                context.Result = new BadRequestObjectResult(exception.Message);
            }
            else if (exception is NoEntityFoundException)
            {
                _logger.LogError("Path: {Path}\nEntity not found: {Message}", requestPath, exception.Message);
                context.Result = new NotFoundObjectResult(exception.Message);
            }
            else if (exception is PatientDoctorMisassignationException)
            {
                _logger.LogError("Path: {Path}\nMisassignation error: {Message}", requestPath, exception.Message);
                context.Result = new BadRequestObjectResult(exception.Message);
            }
            else
            {
                _logger.LogError("Path: {Path}\nAn error occurred: {Message}", requestPath, exception.Message);
                context.Result = new ObjectResult(exception.Message)
                {
                    StatusCode = 500
                };
            }

            context.ExceptionHandled = true;
        }
    }
}
