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

        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var command = new ListAllPatients();
            var patients = await _mediator.Send(command);
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var command = new GetPatientById(id);
            var patient = await _mediator.Send(command);
            return Ok(patient);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddPatient(PatientDto patient)
        {
            var result = Enum.TryParse(patient.Gender, out Gender patientGender);
            if (!result)
            {
                return BadRequest("Invalid gender value for the patient provided");
            }

            var command = new RegisterNewPatient(patient.Name, patient.Surname, patient.Age, patientGender,
                patient.Address, patient.PhoneNumber, patient.InsuranceNumber);

            var newPatient = await _mediator.Send(command);
            return StatusCode(201, newPatient);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> SearchPatientsByASetOfProperties(PatientFilterDto patientFilter)
        {
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
            return Ok(patients);
        }

        [HttpPut("Info/{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdatePatientPersonalInfo(int id, PatientDto patient)
        {
            var result = Enum.TryParse(patient.Gender, out Gender patientGender);
            if (!result)
            {
                return BadRequest("Invalid gender value for the patient provided");
            }

            var command = new UpdatePatientPersonalInfo(id, patient.Name, patient.Surname, patient.Age,
                patientGender, patient.Address, patient.PhoneNumber, patient.InsuranceNumber);

            var updatedPatient = await _mediator.Send(command);
            return Ok(updatedPatient);
        }

        [HttpPut("Doctors/{id}")]
        public async Task<IActionResult> UpdatePatientAssignedDoctors(int id, [FromQuery] List<int> doctorIds)
        {
            var command = new UpdatePatientAssignedDoctors(id, doctorIds);
            var updatedPatient = await _mediator.Send(command);
            return Ok(updatedPatient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var command = new RemoveWronglyAddedPatient(id);
            var deletedPatient = await _mediator.Send(command);
            return Ok(deletedPatient);
        }
    }
}
