using Hospital.Application.MedicalRecords.Commands;
using Hospital.Application.MedicalRecords.Queries;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Presentation.Dto.Record;
using Hospital.Presentation.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DiagnosisMedicalRecordDto = Hospital.Presentation.Dto.Record.DiagnosisMedicalRecordDto;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosisMedicalRecordController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DiagnosisMedicalRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiagnosisMedicalRecords()
        {
            var command = new ListAllDiagnosisMedicalRecords();
            var records = await _mediator.Send(command);
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiagnosisMedicalRecordById(int id)
        {
            var command = new GetDiagnosisMedicalRecordById(id);
            var record = await _mediator.Send(command);
            return Ok(record);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddDiagnosisMedicalRecord(DiagnosisMedicalRecordDto record)
        {
            var command = new AddNewDiagnosisMedicalRecord(record.PatientId, record.DoctorId, record.ExaminationNotes,
                record.IllnessId, record.PrescribedMedicine, record.Duration);

            var newRecord = await _mediator.Send(command);
            return StatusCode(201, newRecord);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> SearchDiagnosisMedicalRecordsByASetOfProperties(DiagnosisMedicalRecordFilterDto recordFilter)
        {
            DiagnosisMedicalRecordFilters df = new DiagnosisMedicalRecordFilters()
            {
                ExaminedPatientId = recordFilter.ExaminedPatientId,
                ResponsibleDoctorId = recordFilter.ResponsibleDoctorId,
                DateOfExamination = recordFilter.DateOfExamination,
                DiagnosedIllnessName = recordFilter.DiagnosedIllnessName,
                PrescribedMedicine = recordFilter.PrescribedMedicine
            };
            var command = new SearchDiagnosisMedicalRecordsByASetOfProperties(df);

            var records = await _mediator.Send(command);
            return Ok(records);
        }

        [HttpPut("ExaminationNotes/{id}")]
        public async Task<IActionResult> UpdateDiagnosisMedicalRecordExaminationNotes(int id, [FromQuery] string notes)
        {
            var command = new AdjustDiagnosisMedicalRecordExaminationNotes(id, notes);
            var updatedRecord = await _mediator.Send(command);
            return Ok(updatedRecord);
        }

        [HttpPut("Treatment/{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateDiagnosisMedicalRecordTreatmentDetails(int id, TreatmentDto treatment)
        {
            var command = new AdjustTreatmentDetailsWithinDiagnosisMedicalRecord(id, treatment.IllnessId,
                treatment.PrescribedMedicine, treatment.Duration);

            var updatedRecord = await _mediator.Send(command);
            return Ok(updatedRecord);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnosisMedicalRecord(int id)
        {
            var command = new RemoveWronglyAddedDiagnosisMedicalRecord(id);
            var deletedRecord = await _mediator.Send(command);
            return Ok(deletedRecord);
        }
    }
}
