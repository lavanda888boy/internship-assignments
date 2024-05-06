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
        private readonly ILogger<DiagnosisMedicalRecordController> _logger;

        public DiagnosisMedicalRecordController(IMediator mediator, ILogger<DiagnosisMedicalRecordController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiagnosisMedicalRecords()
        {
            _logger.LogInformation("Extracting the list of diagnosis records...");

            var command = new ListAllDiagnosisMedicalRecords();
            var records = await _mediator.Send(command);

            _logger.LogInformation("Diagnosis records successfully extracted. List count: {Count}", records.Count);

            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiagnosisMedicalRecordById(int id)
        {
            _logger.LogInformation("Extracting the diagnosis record with id: {Id}...", id);

            var command = new GetDiagnosisMedicalRecordById(id);
            var record = await _mediator.Send(command);

            _logger.LogInformation("Diagnosis record with id: {Id} successfully extracted", id);

            return Ok(record);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddDiagnosisMedicalRecord(DiagnosisMedicalRecordDto record)
        {
            _logger.LogInformation("Adding new diagnosis record: DoctorId = {Doctor}, PatientId = {Patient}...",
                record.DoctorId, record.PatientId);

            var command = new AddNewDiagnosisMedicalRecord(record.PatientId, record.DoctorId, record.ExaminationNotes,
                record.IllnessId, record.PrescribedMedicine, record.Duration);

            var newRecord = await _mediator.Send(command);
            _logger.LogInformation("Diagnosis record: DoctorId = {Doctor}, PatientId = {Patient} was" +
                " successfully added", record.DoctorId, record.PatientId);

            return StatusCode(201, newRecord);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> SearchDiagnosisMedicalRecordsByASetOfProperties(DiagnosisMedicalRecordFilterDto recordFilter)
        {
            _logger.LogInformation("Searching diagnosis records by a set of properties...");

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
            _logger.LogInformation("Diagnosis records with such properties were found. List count: {Count}", records.Count);

            return Ok(records);
        }

        [HttpPut("ExaminationNotes/{id}")]
        public async Task<IActionResult> UpdateDiagnosisMedicalRecordExaminationNotes(int id, [FromQuery] string notes)
        {
            _logger.LogInformation("Updating diagnosis record's (id = {Id}) notes...", id);

            var command = new AdjustDiagnosisMedicalRecordExaminationNotes(id, notes);
            var updatedRecord = await _mediator.Send(command);

            _logger.LogInformation("Diagnosis record's notes successfully updated (id = {Id})", id);

            return Ok(updatedRecord);
        }

        [HttpPut("Treatment/{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateDiagnosisMedicalRecordTreatmentDetails(int id, TreatmentDto treatment)
        {
            _logger.LogInformation("Updating diagnosis record's (id = {Id}) treatment...", id);

            var command = new AdjustTreatmentDetailsWithinDiagnosisMedicalRecord(id, treatment.IllnessId,
                treatment.PrescribedMedicine, treatment.Duration);

            var updatedRecord = await _mediator.Send(command);
            _logger.LogInformation("Diagnosis record's treatment successfully updated (id = {Id})", id);

            return Ok(updatedRecord);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnosisMedicalRecord(int id)
        {
            _logger.LogInformation("Deleting diagnosis record with id: {Id}...", id);

            var command = new RemoveWronglyAddedDiagnosisMedicalRecord(id);
            var deletedRecord = await _mediator.Send(command);

            _logger.LogInformation("Diagnosis record with id: {Id} was successfully deleted", id);

            return Ok(deletedRecord);
        }
    }
}
