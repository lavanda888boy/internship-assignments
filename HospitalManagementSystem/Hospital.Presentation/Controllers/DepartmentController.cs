using Hospital.Application.Departments.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, DoctorUser, PatientUser")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var command = new ListAllPaginatedDepartments();
            var paginatedDepartments = await _mediator.Send(command);

            return Ok(paginatedDepartments.Items);
        }
    }
}
