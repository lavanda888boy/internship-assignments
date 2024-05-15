using Hospital.Application.Patients.Commands;
using Hospital.Application.Patients.Queries;
using Hospital.Presentation.Dto.Patient;
using Hospital.Presentation.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> GetAllPatients()
        {
            _logger.LogInformation("Extracting the list of patients...");

            var command = new ListAllPatients();
            var patients = await _mediator.Send(command);

            _logger.LogInformation("Patients successfully extracted. List count: {Count}", patients.Count);

            return Ok(patients);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            _logger.LogInformation("Extracting the patient with id: {Id}...", id);

            var command = new GetPatientById(id);
            var patient = await _mediator.Send(command);

            _logger.LogInformation("Patient with id: {Id} successfully extracted", id);

            return Ok(patient);
        }

        [HttpPost]
        [ServiceFilter(typeof(ModelValidationFilter))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPatient(PatientRequestDto patient)
        {
            _logger.LogInformation("Adding new patient: {Name} {Surname}...", patient.Name, patient.Surname);

            var command = new RegisterNewPatient(patient.Name, patient.Surname, patient.Age, patient.Gender,
                patient.Address, patient.PhoneNumber, patient.InsuranceNumber);

            var newPatient = await _mediator.Send(command);
            _logger.LogInformation("Patient: {Name} {Surname} was successfully added", patient.Name, patient.Surname);

            return StatusCode(201, newPatient);
        }

        [HttpPost("Search")]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> SearchPatientsByASetOfProperties(PatientFilterRequestDto patientFilter)
        {
            _logger.LogInformation("Searching patients by a set of properties...");

            var command = new SearchPatientsByASetOfProperties(patientFilter.Name, patientFilter.Surname,
                patientFilter.Age, patientFilter.Gender, patientFilter.Address, patientFilter.PhoneNumber,
                patientFilter.InsuranceNumber);

            var patients = await _mediator.Send(command);
            _logger.LogInformation("Patients with such properties were found. List count: {Id}", patients.Count);

            return Ok(patients);
        }

        [HttpPut("Info/{id}")]
        [ServiceFilter(typeof(ModelValidationFilter))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePatientPersonalInfo(int id, PatientRequestDto patient)
        {
            _logger.LogInformation("Updating patient's (id = {Id}) personal info...", id);

            var command = new UpdatePatientPersonalInfo(id, patient.Name, patient.Surname, patient.Age,
                patient.Gender, patient.Address, patient.PhoneNumber, patient.InsuranceNumber);

            var updatedPatient = await _mediator.Send(command);
            _logger.LogInformation("Patient's personal info successfully updated (id = {Id})", id);

            return Ok(updatedPatient);
        }

        [HttpPut("Doctors/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePatientAssignedDoctors(int id, [FromQuery] List<int> doctorIds)
        {
            _logger.LogInformation("Updating patient's (id = {Id}) assigned doctors...", id);

            var command = new UpdatePatientAssignedDoctors(id, doctorIds);
            var updatedPatient = await _mediator.Send(command);

            _logger.LogInformation("Patient's assigned doctors successfully updated (id = {Id})", id);

            return Ok(updatedPatient);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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

