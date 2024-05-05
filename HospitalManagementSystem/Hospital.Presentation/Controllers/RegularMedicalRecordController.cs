using Hospital.Application.MedicalRecords.Commands;
using Hospital.Application.MedicalRecords.Queries;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Presentation.Dto.Record;
using Hospital.Presentation.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RegularMedicalRecordDto = Hospital.Presentation.Dto.Record.RegularMedicalRecordDto;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegularMedicalRecordController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegularMedicalRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegularMedicalRecords()
        {
            var command = new ListAllRegularMedicalRecords();
            var records = await _mediator.Send(command);
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegularMedicalRecordById(int id)
        {
            var command = new GetRegularMedicalRecordById(id);
            var record = await _mediator.Send(command);
            return Ok(record);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddRegularMedicalRecord(RegularMedicalRecordDto record)
        {
            var command = new AddNewRegularMedicalRecord(record.PatientId, record.DoctorId,
                record.ExaminationNotes);

            var newRecord = await _mediator.Send(command);
            return StatusCode(201, newRecord);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> SearchRegularMedicalRecordsByASetOfProperties(RegularMedicalRecordFilterDto recordFilter)
        {
            RegularMedicalRecordFilters rf = new RegularMedicalRecordFilters()
            {
                ExaminedPatientId = recordFilter.ExaminedPatientId,
                ResponsibleDoctorId = recordFilter.ResponsibleDoctorId,
                DateOfExamination = recordFilter.DateOfExamination
            };
            var command = new SearchRegularMedicalRecordsByASetProperties(rf);

            var records = await _mediator.Send(command);
            return Ok(records);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegularMedicalRecordExaminationNotes(int id, [FromQuery] string notes)
        {
            var command = new AdjustRegularMedicalRecordExaminationNotes(id, notes);
            var updatedRecord = await _mediator.Send(command);
            return Ok(updatedRecord);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegularMedicalRecord(int id)
        {
            var command = new RemoveWronglyAddedRegularMedicalRecord(id);
            var deletedRecord = await _mediator.Send(command);
            return Ok(deletedRecord);
        }
    }
}
