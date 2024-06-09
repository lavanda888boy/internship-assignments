using Hospital.Application.MedicalRecords.Commands;
using Hospital.Application.MedicalRecords.Queries;
using Hospital.Presentation.Dto.Record;
using Hospital.Presentation.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RegularMedicalRecordController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegularMedicalRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> GetPaginatedRegularMedicalRecords([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var command = new ListAllPaginatedRegularMedicalRecords(pageNumber, pageSize);
            var paginatedRecords = await _mediator.Send(command);

            return Ok(paginatedRecords);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> GetRegularMedicalRecordById(int id)
        {
            var command = new GetRegularMedicalRecordById(id);
            var record = await _mediator.Send(command);

            return Ok(record);
        }

        [HttpPost]
        [ServiceFilter(typeof(ModelValidationFilter))]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> AddRegularMedicalRecord(RegularMedicalRecordRequestDto record)
        {
            var command = new AddNewRegularMedicalRecord(record.PatientId, record.DoctorId,
                record.ExaminationNotes);

            var newRecord = await _mediator.Send(command);

            return StatusCode(201, newRecord);
        }

        [HttpPost("Search")]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> SearchRegularMedicalRecordsByASetOfProperties([FromQuery] int pageNumber,
            [FromQuery] int pageSize, [FromBody] RegularMedicalRecordFilterRequestDto recordFilter)
        {
            var command = new SearchRegularMedicalRecordsByASetPropertiesPaginated(pageNumber,
                pageSize, recordFilter.ExaminedPatientId, recordFilter.ResponsibleDoctorId,
                recordFilter.DateOfExamination);

            var records = await _mediator.Send(command);

            return Ok(records);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> UpdateRegularMedicalRecordExaminationNotes(int id, [FromQuery] string notes)
        {
            var command = new AdjustRegularMedicalRecordExaminationNotes(id, notes);
            var updatedRecord = await _mediator.Send(command);

            return Ok(updatedRecord);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRegularMedicalRecord(int id)
        {
            var command = new RemoveWronglyAddedRegularMedicalRecord(id);
            var deletedRecord = await _mediator.Send(command);

            return Ok(deletedRecord);
        }
    }
}
