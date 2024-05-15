﻿using Hospital.Application.MedicalRecords.Commands;
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
        private readonly ILogger<DiagnosisMedicalRecordController> _logger;

        public DiagnosisMedicalRecordController(IMediator mediator, ILogger<DiagnosisMedicalRecordController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> GetAllDiagnosisMedicalRecords()
        {
            _logger.LogInformation("Extracting the list of diagnosis records...");

            var command = new ListAllDiagnosisMedicalRecords();
            var records = await _mediator.Send(command);

            _logger.LogInformation("Diagnosis records successfully extracted. List count: {Count}", records.Count);

            return Ok(records);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> GetDiagnosisMedicalRecordById(int id)
        {
            _logger.LogInformation("Extracting the diagnosis record with id: {Id}...", id);

            var command = new GetDiagnosisMedicalRecordById(id);
            var record = await _mediator.Send(command);

            _logger.LogInformation("Diagnosis record with id: {Id} successfully extracted", id);

            return Ok(record);
        }

        [HttpPost]
        [ServiceFilter(typeof(ModelValidationFilter))]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> AddDiagnosisMedicalRecord(DiagnosisMedicalRecordRequestDto record)
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
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> SearchDiagnosisMedicalRecordsByASetOfProperties(DiagnosisMedicalRecordFilterRequestDto recordFilter)
        {
            _logger.LogInformation("Searching diagnosis records by a set of properties...");

            var command = new SearchDiagnosisMedicalRecordsByASetOfProperties(recordFilter.ExaminedPatientId,
                recordFilter.ResponsibleDoctorId, recordFilter.DateOfExamination, recordFilter.DiagnosedIllnessName,
                recordFilter.PrescribedMedicine);

            var records = await _mediator.Send(command);
            _logger.LogInformation("Diagnosis records with such properties were found. List count: {Count}", records.Count);

            return Ok(records);
        }

        [HttpPut("ExaminationNotes/{id}")]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> UpdateDiagnosisMedicalRecordExaminationNotes(int id, [FromQuery] string notes)
        {
            _logger.LogInformation("Updating diagnosis record's (id = {Id}) notes...", id);

            var command = new AdjustDiagnosisMedicalRecordExaminationNotes(id, notes);
            var updatedRecord = await _mediator.Send(command);

            _logger.LogInformation("Diagnosis record's notes successfully updated (id = {Id})", id);

            return Ok(updatedRecord);
        }

        [HttpPut("Treatment/{id}")]
        [ServiceFilter(typeof(ModelValidationFilter))]
        [Authorize(Roles = "Admin, DoctorUser")]
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
        [Authorize(Roles = "Admin")]
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
