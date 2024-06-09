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
    public class DiagnosisMedicalRecordController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DiagnosisMedicalRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> GetPaginatedDiagnosisMedicalRecords([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var command = new ListAllPaginatedDiagnosisMedicalRecords(pageNumber, pageSize);
            var paginatedRecords = await _mediator.Send(command);

            return Ok(paginatedRecords);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> GetDiagnosisMedicalRecordById(int id)
        {
            var command = new GetDiagnosisMedicalRecordById(id);
            var record = await _mediator.Send(command);

            return Ok(record);
        }

        [HttpPost]
        [ServiceFilter(typeof(ModelValidationFilter))]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> AddDiagnosisMedicalRecord(DiagnosisMedicalRecordRequestDto record)
        {
            var command = new AddNewDiagnosisMedicalRecord(record.PatientId, record.DoctorId, record.ExaminationNotes,
                record.IllnessId, record.PrescribedMedicine, record.Duration);

            var newRecord = await _mediator.Send(command);

            return StatusCode(201, newRecord);
        }

        [HttpPost("Search")]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> SearchDiagnosisMedicalRecordsByASetOfProperties([FromQuery] int pageNumber,
            [FromQuery] int pageSize, [FromBody] DiagnosisMedicalRecordFilterRequestDto recordFilter)
        {
            var command = new SearchDiagnosisMedicalRecordsByASetOfPropertiesPaginated(pageNumber,
                pageSize, recordFilter.ExaminedPatientId, recordFilter.ResponsibleDoctorId,
                recordFilter.DateOfExamination, recordFilter.DiagnosedIllnessName, recordFilter.PrescribedMedicine);

            var records = await _mediator.Send(command);

            return Ok(records);
        }

        [HttpPut("ExaminationNotes/{id}")]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> UpdateDiagnosisMedicalRecordExaminationNotes(int id, [FromQuery] string notes)
        {
            var command = new AdjustDiagnosisMedicalRecordExaminationNotes(id, notes);
            var updatedRecord = await _mediator.Send(command);

            return Ok(updatedRecord);
        }

        [HttpPut("Treatment/{id}")]
        [ServiceFilter(typeof(ModelValidationFilter))]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> UpdateDiagnosisMedicalRecordTreatmentDetails(int id, TreatmentDto treatment)
        {
            var command = new AdjustTreatmentDetailsWithinDiagnosisMedicalRecord(id, treatment.IllnessId,
                treatment.PrescribedMedicine, treatment.Duration);

            var updatedRecord = await _mediator.Send(command);

            return Ok(updatedRecord);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDiagnosisMedicalRecord(int id)
        {
            var command = new RemoveWronglyAddedDiagnosisMedicalRecord(id);
            var deletedRecord = await _mediator.Send(command);

            return Ok(deletedRecord);
        }
    }
}
