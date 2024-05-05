using Hospital.Application.Doctors.Commands;
using Hospital.Application.Doctors.Queries;
using Hospital.Application.Doctors.Responses;
using Hospital.Presentation.Dto.Doctor;
using Hospital.Presentation.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DoctorDto = Hospital.Presentation.Dto.Doctor.DoctorDto;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DoctorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var command = new ListAllDoctors();
            var doctors = await _mediator.Send(command);
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var command = new GetDoctorById(id);
            var doctor = await _mediator.Send(command);
            return Ok(doctor);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddDoctor(DoctorDto doctor)
        {
            var command = new EmployNewDoctor(doctor.Name, doctor.Surname, doctor.Address, doctor.PhoneNumber, doctor.DepartmentId,
                TimeSpan.Parse(doctor.StartShift), TimeSpan.Parse(doctor.EndShift), doctor.WeekDayIds);

            var newDoctor = await _mediator.Send(command);
            return StatusCode(201, newDoctor);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> SearchDoctorsByASetOfProperties(DoctorFilterDto doctorFilter)
        {
            DoctorFilters df = new DoctorFilters()
            {
                Name = doctorFilter.Name,
                Surname = doctorFilter.Surname,
                Address = doctorFilter.Address,
                PhoneNumber = doctorFilter.PhoneNumber,
                DepartmentName = doctorFilter.DepartmentName
            };
            var command = new SearchDoctorsByASetOfProperties(df);

            var doctors = await _mediator.Send(command);
            return Ok(doctors);
        }

        [HttpPut("Info/{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateDoctorPersonalInfo(int id, DoctorDto doctor)
        {
            var command = new UpdateDoctorPersonalInfo(id, doctor.Name, doctor.Surname, doctor.Address, doctor.PhoneNumber,
                doctor.DepartmentId, TimeSpan.Parse(doctor.StartShift), TimeSpan.Parse(doctor.EndShift), doctor.WeekDayIds);

            var updatedDoctor = await _mediator.Send(command);
            return Ok(updatedDoctor);
        }

        [HttpPut("Patients/{id}")]
        public async Task<IActionResult> UpdateDoctorAssignedPatients(int id, [FromQuery] List<int> patientIds)
        {
            var command = new UpdateDoctorAssignedPatients(id, patientIds);
            var updatedDoctor = await _mediator.Send(command);
            return Ok(updatedDoctor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var command = new RemoveWronglyEmployedDoctor(id);
            var deletedDoctor = await _mediator.Send(command);
            return Ok(deletedDoctor);
        }
    }
}
