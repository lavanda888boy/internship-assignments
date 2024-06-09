using Hospital.Application.Doctors.Commands;
using Hospital.Application.Doctors.Queries;
using Hospital.Presentation.Dto.Doctor;
using Hospital.Presentation.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DoctorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DoctorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, DoctorUser, PatientUser")]
        public async Task<IActionResult> GetPaginatedDoctors([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var command = new ListAllPaginatedDoctors(pageNumber, pageSize);
            var paginatedDoctors = await _mediator.Send(command);

            return Ok(paginatedDoctors);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, DoctorUser, PatientUser")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var command = new GetDoctorById(id);
            var doctor = await _mediator.Send(command);

            return Ok(doctor);
        } 

        [HttpPost]
        [ServiceFilter(typeof(ModelValidationFilter))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddDoctor(DoctorRequestDto doctor)
        {
            var command = new EmployNewDoctor(doctor.Name, doctor.Surname, doctor.Address, doctor.PhoneNumber, doctor.DepartmentId,
                TimeSpan.Parse(doctor.StartShift), TimeSpan.Parse(doctor.EndShift), doctor.WeekDayIds);

            var newDoctor = await _mediator.Send(command);

            return StatusCode(201, newDoctor);
        }

        [HttpPost("Search")]
        [Authorize(Roles = "Admin, DoctorUser, PatientUser")]
        public async Task<IActionResult> SearchDoctorsByASetOfProperties([FromQuery] int pageNumber, 
            [FromQuery] int pageSize, [FromBody] DoctorFilterRequestDto doctorFilter)
        {
            var command = new SearchDoctorsByASetOfPropertiesPaginated(pageNumber, pageSize, 
                doctorFilter.Name, doctorFilter.Surname, doctorFilter.Address, doctorFilter.PhoneNumber,
                doctorFilter.DepartmentName);

            var doctors = await _mediator.Send(command);

            return Ok(doctors);
        }

        [HttpPut("Info/{id}")]
        [ServiceFilter(typeof(ModelValidationFilter))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDoctorPersonalInfo(int id, DoctorRequestDto doctor)
        {
            var command = new UpdateDoctorPersonalInfo(id, doctor.Name, doctor.Surname, doctor.Address, doctor.PhoneNumber,
                doctor.DepartmentId, TimeSpan.Parse(doctor.StartShift), TimeSpan.Parse(doctor.EndShift), doctor.WeekDayIds);

            var updatedDoctor = await _mediator.Send(command);

            return Ok(updatedDoctor);
        }

        [HttpPut("Patients/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDoctorAssignedPatients(int id, [FromQuery] List<int> patientIds)
        {
            var command = new UpdateDoctorAssignedPatients(id, patientIds);
            var updatedDoctor = await _mediator.Send(command);

            return Ok(updatedDoctor);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var command = new RemoveWronglyEmployedDoctor(id);
            var deletedDoctor = await _mediator.Send(command);

            return Ok(deletedDoctor);
        }
    }
}

