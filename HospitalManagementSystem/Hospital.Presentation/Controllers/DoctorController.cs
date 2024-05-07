using Hospital.Application.Doctors.Commands;
using Hospital.Application.Doctors.Queries;
using Hospital.Presentation.Dto.Doctor;
using Hospital.Presentation.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(IMediator mediator, ILogger<DoctorController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            _logger.LogInformation("Extracting the list of doctors...");

            var command = new ListAllDoctors();
            var doctors = await _mediator.Send(command);

            _logger.LogInformation("Doctors successfully extracted. List count: {Count}", doctors.Count);

            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            _logger.LogInformation("Extracting the doctor with id: {Id}...", id);

            var command = new GetDoctorById(id);
            var doctor = await _mediator.Send(command);

            _logger.LogInformation("Doctor with id: {Id} successfully extracted", id);

            return Ok(doctor);
        }

        [HttpPost]
        [ServiceFilter(typeof(ModelValidationFilter))]
        public async Task<IActionResult> AddDoctor(DoctorRequestDto doctor)
        {
            _logger.LogInformation("Adding new doctor: {Name} {Surname}...", doctor.Name, doctor.Surname);

            var command = new EmployNewDoctor(doctor.Name, doctor.Surname, doctor.Address, doctor.PhoneNumber, doctor.DepartmentId,
                TimeSpan.Parse(doctor.StartShift), TimeSpan.Parse(doctor.EndShift), doctor.WeekDayIds);

            var newDoctor = await _mediator.Send(command);
            _logger.LogInformation("Doctor: {Name} {Surname} was successfully added", doctor.Name, doctor.Surname);

            return StatusCode(201, newDoctor);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> SearchDoctorsByASetOfProperties(DoctorFilterRequestDto doctorFilter)
        {
            _logger.LogInformation("Searching doctors by a set of properties...");

            var command = new SearchDoctorsByASetOfProperties(doctorFilter.Name, doctorFilter.Surname,
                doctorFilter.Address, doctorFilter.PhoneNumber, doctorFilter.DepartmentName);

            var doctors = await _mediator.Send(command);
            _logger.LogInformation("Doctors with such properties were found. List count: {Count}", doctors.Count);

            return Ok(doctors);
        }

        [HttpPut("Info/{id}")]
        [ServiceFilter(typeof(ModelValidationFilter))]
        public async Task<IActionResult> UpdateDoctorPersonalInfo(int id, DoctorRequestDto doctor)
        {
            _logger.LogInformation("Updating doctor's (id = {Id}) personal info...", id);

            var command = new UpdateDoctorPersonalInfo(id, doctor.Name, doctor.Surname, doctor.Address, doctor.PhoneNumber,
                doctor.DepartmentId, TimeSpan.Parse(doctor.StartShift), TimeSpan.Parse(doctor.EndShift), doctor.WeekDayIds);

            var updatedDoctor = await _mediator.Send(command);
            _logger.LogInformation("Doctor's personal info successfully updated (id = {Id})", id);

            return Ok(updatedDoctor);
        }

        [HttpPut("Patients/{id}")]
        public async Task<IActionResult> UpdateDoctorAssignedPatients(int id, [FromQuery] List<int> patientIds)
        {
            _logger.LogInformation("Updating doctor's (id = {Id}) assigned patients...", id);

            var command = new UpdateDoctorAssignedPatients(id, patientIds);
            var updatedDoctor = await _mediator.Send(command);

            _logger.LogInformation("Doctor's assigned patients successfully updated (id = {Id})", id);

            return Ok(updatedDoctor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            _logger.LogInformation("Deleting doctor with id: {Id}...", id);

            var command = new RemoveWronglyEmployedDoctor(id);
            var deletedDoctor = await _mediator.Send(command);

            _logger.LogInformation("Doctor with id: {Id} was successfully deleted", id);

            return Ok(deletedDoctor);
        }
    }
}

