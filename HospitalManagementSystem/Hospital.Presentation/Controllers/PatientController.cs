using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Commands;
using Hospital.Application.Patients.Queries;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models.Utility;
using Hospital.Presentation.Dto.Patient;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
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

            try
            {
                var patients = await _mediator.Send(command);
                return Ok(patients);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var command = new GetPatientById(id);

            try
            {
                var patient = await _mediator.Send(command);
                return Ok(patient);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (NoEntityFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient(PatientDto patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Patient data for creation is invalid");
            }

            var result = Enum.TryParse(patient.Gender, out Gender patientGender);
            if (!result)
            {
                return BadRequest("Invalid gender value for the patient provided");
            }

            var command = new RegisterNewPatient(patient.Name, patient.Surname, patient.Age, patientGender,
                patient.Address, patient.PhoneNumber, patient.InsuranceNumber);

            try
            {
                var newPatient = await _mediator.Send(command);
                return StatusCode(201, newPatient);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (NoEntityFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("search")]
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

            try
            {
                var patients = await _mediator.Send(command);
                return Ok(patients);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (NoEntityFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatientPersonalInfo(int id, PatientDto patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Patient data for update is invalid");
            }

            var result = Enum.TryParse(patient.Gender, out Gender patientGender);
            if (!result)
            {
                return BadRequest("Invalid gender value for the patient provided");
            }

            var command = new UpdatePatientPersonalInfo(id, patient.Name, patient.Surname, patient.Age,
                patientGender, patient.Address, patient.PhoneNumber, patient.InsuranceNumber);

            try
            {
                var updatedPatient = await _mediator.Send(command);
                return Ok(updatedPatient);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (NoEntityFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("doctors/{id}")]
        public async Task<IActionResult> UpdatePatientAssignedDoctors(int id, [FromQuery] List<int> doctorIds)
        {
            var command = new UpdatePatientAssignedDoctors(id, doctorIds);

            try
            {
                var updatedPatient = await _mediator.Send(command);
                return Ok(updatedPatient);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (NoEntityFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var command = new RemoveWronglyAddedPatient(id);

            try
            {
                var deletedPatient = await _mediator.Send(command);
                return Ok(deletedPatient);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (NoEntityFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
