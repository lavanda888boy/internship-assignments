using Hospital.Application.AI.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class MedicalAdviceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicalAdviceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "PatientUser")]
        public async Task<IActionResult> GetMedicalAdviceForPatientById(int id)
        {
            var command = new GenerateMedicalAdviceForPatient(id);
            var advice = await _mediator.Send(command);

            return Ok(advice);
        }
    }
}
