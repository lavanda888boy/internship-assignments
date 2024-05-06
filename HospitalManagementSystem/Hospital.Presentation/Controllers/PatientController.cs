using Hospital.Application.Patients.Commands;
using Hospital.Application.Patients.Queries;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models.Utility;
using Hospital.Presentation.Dto.Patient;
using Hospital.Presentation.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PatientDto = Hospital.Presentation.Dto.Patient.PatientDto;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PatientController> _logger;

        public PatientController(IMediator mediator, ILogger<PatientController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            _logger.LogInformation("Extracting the list of patients...");

            var command = new ListAllPatients();
            var patients = await _mediator.Send(command);

            _logger.LogInformation("Patients successfully extracted. List count: {Count}", patients.Count);

            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            _logger.LogInformation("Extracting the patient with id: {Id}...", id);

            var command = new GetPatientById(id);
            var patient = await _mediator.Send(command);

            _logger.LogInformation("Patient with id: {Id} successfully extracted", id);

            return Ok(patient);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddPatient(PatientDto patient)
        {
            _logger.LogInformation("Adding new patient: {Name} {Surname}...", patient.Name, patient.Surname);

            var result = Enum.TryParse(patient.Gender, out Gender patientGender);
            if (!result)
            {
                return BadRequest("Invalid gender value for the patient provided");
            }

            var command = new RegisterNewPatient(patient.Name, patient.Surname, patient.Age, patientGender,
                patient.Address, patient.PhoneNumber, patient.InsuranceNumber);

            var newPatient = await _mediator.Send(command);
            _logger.LogInformation("Patient: {Name} {Surname} was successfully added", patient.Name, patient.Surname);

            return StatusCode(201, newPatient);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> SearchPatientsByASetOfProperties(PatientFilterDto patientFilter)
        {
            _logger.LogInformation("Searching patients by a set of properties...");

            var result = Enum.TryParse(patientFilter.Gender, out Gender patientGender);
            if (!result)
            {
                return BadRequest("Invalid gender value for the patient provided");
            }

            PatientFilters pf = new PatientFilters()
            {
                Name = patientFilter.Name,
                Surname = patientFilter.Surname,
                Age = patientFilter.Age,
                Gender = patientGender,
                Address = patientFilter.Address,
                PhoneNumber = patientFilter.PhoneNumber,
                InsuranceNumber = patientFilter.InsuranceNumber
            };
            var command = new SearchPatientsByASetOfProperties(pf);

            var patients = await _mediator.Send(command);
            _logger.LogInformation("Patients with such properties were found. List count: {Id}", patients.Count);

            return Ok(patients);
        }

        [HttpPut("Info/{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdatePatientPersonalInfo(int id, PatientDto patient)
        {
            _logger.LogInformation("Updating patient's (id = {Id}) personal info...", id);

            var result = Enum.TryParse(patient.Gender, out Gender patientGender);
            if (!result)
            {
                return BadRequest("Invalid gender value for the patient provided");
            }

            var command = new UpdatePatientPersonalInfo(id, patient.Name, patient.Surname, patient.Age,
                patientGender, patient.Address, patient.PhoneNumber, patient.InsuranceNumber);

            var updatedPatient = await _mediator.Send(command);
            _logger.LogInformation("Patient's personal info successfully updated (id = {Id})", id);

            return Ok(updatedPatient);
        }

        [HttpPut("Doctors/{id}")]
        public async Task<IActionResult> UpdatePatientAssignedDoctors(int id, [FromQuery] List<int> doctorIds)
        {
            _logger.LogInformation("Updating patient's (id = {Id}) assigned doctors...", id);

            var command = new UpdatePatientAssignedDoctors(id, doctorIds);
            var updatedPatient = await _mediator.Send(command);

            _logger.LogInformation("Patient's assigned doctors successfully updated (id = {Id})", id);

            return Ok(updatedPatient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            _logger.LogInformation("Deleting patient with id: {Id}...", id);

            var command = new RemoveWronglyAddedPatient(id);
            var deletedPatient = await _mediator.Send(command);

            _logger.LogInformation("Patient with id: {Id} was successfully deleted", id);

            return Ok(deletedPatient);
        }
    }
}

