using Hospital.Application.MedicalRecords.Commands;
using Hospital.Application.MedicalRecords.Queries;
using Hospital.Presentation.Dto.Record;
using Hospital.Presentation.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegularMedicalRecordController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RegularMedicalRecordController> _logger;

        public RegularMedicalRecordController(IMediator mediator, ILogger<RegularMedicalRecordController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegularMedicalRecords()
        {
            _logger.LogInformation("Extracting the list of regular records...");

            var command = new ListAllRegularMedicalRecords();
            var records = await _mediator.Send(command);

            _logger.LogInformation("Regular records successfully extracted. List count: {Count}", records.Count);

            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegularMedicalRecordById(int id)
        {
            _logger.LogInformation("Extracting the regular record with id: {Id}...", id);

            var command = new GetRegularMedicalRecordById(id);
            var record = await _mediator.Send(command);

            _logger.LogInformation("Regular record with id: {Id} successfully extracted", id);

            return Ok(record);
        }

        [HttpPost]
        [ServiceFilter(typeof(ModelValidationFilter))]
        public async Task<IActionResult> AddRegularMedicalRecord(RegularMedicalRecordRequestDto record)
        {
            _logger.LogInformation("Adding new regular record: DoctorId = {Doctor}, PatientId = {Patient}...",
                record.DoctorId, record.PatientId);

            var command = new AddNewRegularMedicalRecord(record.PatientId, record.DoctorId,
                record.ExaminationNotes);

            var newRecord = await _mediator.Send(command);
            _logger.LogInformation("Regular record: DoctorId = {Doctor}, PatientId = {Patient} was" +
                " successfully added", record.DoctorId, record.PatientId);

            return StatusCode(201, newRecord);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> SearchRegularMedicalRecordsByASetOfProperties(RegularMedicalRecordFilterRequestDto recordFilter)
        {
            _logger.LogInformation("Searching regular records by a set of properties...");

            var command = new SearchRegularMedicalRecordsByASetProperties(recordFilter.ExaminedPatientId,
                recordFilter.ResponsibleDoctorId, recordFilter.DateOfExamination);

            var records = await _mediator.Send(command);
            _logger.LogInformation("Regular records with such properties were found. List count: {Count}", records.Count);

            return Ok(records);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegularMedicalRecordExaminationNotes(int id, [FromQuery] string notes)
        {
            _logger.LogInformation("Updating regular record's (id = {Id}) notes...", id);

            var command = new AdjustRegularMedicalRecordExaminationNotes(id, notes);
            var updatedRecord = await _mediator.Send(command);

            _logger.LogInformation("Regular record's notes successfully updated (id = {Id})", id);

            return Ok(updatedRecord);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegularMedicalRecord(int id)
        {
            _logger.LogInformation("Deleting regular record with id: {Id}...", id);

            var command = new RemoveWronglyAddedRegularMedicalRecord(id);
            var deletedRecord = await _mediator.Send(command);

            _logger.LogInformation("Regular record with id: {Id} was successfully deleted", id);

            return Ok(deletedRecord);
        }
    }
}
