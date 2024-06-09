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

        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> GetPaginatedPatients([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var command = new ListAllPaginatedPatients(pageNumber, pageSize);
            var paginatedPatients = await _mediator.Send(command);

            return Ok(paginatedPatients);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var command = new GetPatientById(id);
            var patient = await _mediator.Send(command);

            return Ok(patient);
        }

        [HttpPost]
        [ServiceFilter(typeof(ModelValidationFilter))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPatient(PatientRequestDto patient)
        {
            var command = new RegisterNewPatient(patient.Name, patient.Surname, patient.Age, patient.Gender,
                patient.Address, patient.PhoneNumber, patient.InsuranceNumber);

            var newPatient = await _mediator.Send(command);

            return StatusCode(201, newPatient);
        }

        [HttpPost("Search")]
        [Authorize(Roles = "Admin, DoctorUser")]
        public async Task<IActionResult> SearchPatientsByASetOfProperties([FromQuery] int pageNumber,
            [FromQuery] int pageSize, [FromBody] PatientFilterRequestDto patientFilter)
        {
            var command = new SearchPatientsByASetOfPropertiesPaginated(pageNumber, pageSize, patientFilter.Name, patientFilter.Surname,
                patientFilter.Age, patientFilter.Gender, patientFilter.Address, patientFilter.PhoneNumber,
                patientFilter.InsuranceNumber);

            var patients = await _mediator.Send(command);

            return Ok(patients);
        }

        [HttpPut("Info/{id}")]
        [ServiceFilter(typeof(ModelValidationFilter))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePatientPersonalInfo(int id, PatientRequestDto patient)
        {
            var command = new UpdatePatientPersonalInfo(id, patient.Name, patient.Surname, patient.Age,
                patient.Gender, patient.Address, patient.PhoneNumber, patient.InsuranceNumber);

            var updatedPatient = await _mediator.Send(command);

            return Ok(updatedPatient);
        }

        [HttpPut("Doctors/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePatientAssignedDoctors(int id, [FromQuery] List<int> doctorIds)
        {
            var command = new UpdatePatientAssignedDoctors(id, doctorIds);
            var updatedPatient = await _mediator.Send(command);

            return Ok(updatedPatient);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var command = new RemoveWronglyAddedPatient(id);
            var deletedPatient = await _mediator.Send(command);

            return Ok(deletedPatient);
        }
    }
}

