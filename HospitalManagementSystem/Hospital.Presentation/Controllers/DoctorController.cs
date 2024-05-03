using Hospital.Application.Doctors.Commands;
using Hospital.Application.Doctors.Queries;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Presentation.Dto.Doctor;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
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

            try
            {
                var doctors = await _mediator.Send(command);
                return Ok(doctors);
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
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var command = new GetDoctorById(id);

            try
            {
                var doctor = await _mediator.Send(command);
                return Ok(doctor);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (NoEntityFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor(DoctorDto doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Doctor data for creation is invalid");
            }

            var command = new EmployNewDoctor(doctor.Name, doctor.Surname, doctor.Address, doctor.PhoneNumber, doctor.DepartmentId,
                TimeSpan.Parse(doctor.StartShift), TimeSpan.Parse(doctor.EndShift), doctor.WeekDayIds);

            try
            {
                var newDoctor = await _mediator.Send(command);
                return StatusCode(201, newDoctor);
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

            try
            {
                var doctors = await _mediator.Send(command);
                return Ok(doctors);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (NoEntityFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Info/{id}")]
        public async Task<IActionResult> UpdateDoctorPersonalInfo(int id, DoctorDto doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Doctor data for update is invalid");
            }

            var command = new UpdateDoctorPersonalInfo(id, doctor.Name, doctor.Surname, doctor.Address, doctor.PhoneNumber,
                doctor.DepartmentId, TimeSpan.Parse(doctor.StartShift), TimeSpan.Parse(doctor.EndShift), doctor.WeekDayIds);

            try
            {
                var updatedDoctor = await _mediator.Send(command);
                return Ok(updatedDoctor);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (NoEntityFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Patients/{id}")]
        public async Task<IActionResult> UpdateDoctorAssignedPatients(int id, [FromQuery] List<int> patientIds)
        {
            var command = new UpdateDoctorAssignedPatients(id, patientIds);

            try
            {
                var updatedDoctor = await _mediator.Send(command);
                return Ok(updatedDoctor);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (NoEntityFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var command = new RemoveWronglyEmployedDoctor(id);

            try
            {
                var deletedDoctor = await _mediator.Send(command);
                return Ok(deletedDoctor);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (NoEntityFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
