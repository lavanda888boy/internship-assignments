using Hospital.Application.Illnesses.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class IllnessController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IllnessController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> GetAllIllnesses()
        {
            var command = new ListAllPaginatedIllnesses();
            var paginatedIllnesses = await _mediator.Send(command);

            return Ok(paginatedIllnesses.Items);
        }
    }
}
