using Hospital.Infrastructure;
using Hospital.Presentation.Dto.Patient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly HospitalManagementDbContext _context = new HospitalManagementDbContext();

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _context.Patients.Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Surname = p.Surname,
                Age = p.Age,
                Doctors = p.DoctorsPatients.Select(dp => dp.Doctor.Name)
                                           .ToList()
            }).ToListAsync();

            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = await _context.Patients.SingleOrDefaultAsync(p => p.Id == id);
            return Ok(patient);
        }

        //[HttpPost]
        //public async Task<IActionResult> SearchPatientsByASetOfProperties(PatientFilterDto patientFilter)
        //{
        //    Expression<Func<Patient, bool>> predicate = p =>
        //        (string.IsNullOrEmpty(patientFilter.Name) || p.Name == patientFilter.Name) &&
        //        (string.IsNullOrEmpty(patientFilter.Surname) || p.Surname == patientFilter.Surname) &&
        //        (patientFilter.Age == 0 || p.Age == patientFilter.Age) &&
        //        (string.IsNullOrEmpty(patientFilter.Gender) || p.Gender == patientFilter.Gender) &&
        //        (string.IsNullOrEmpty(patientFilter.Address) || p.Address == patientFilter.Address) &&
        //        (string.IsNullOrEmpty(patientFilter.PhoneNumber) || p.PhoneNumber == patientFilter.PhoneNumber) &&
        //        (string.IsNullOrEmpty(patientFilter.InsuranceNumber) || p.InsuranceNumber == patientFilter.InsuranceNumber);

        //    var patients = _context.Patients.Where(predicate.Compile());

        //    return Ok(patients);
        //}

        [HttpPost]
        public async Task<IActionResult> AddPatient(PatientDto patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Patient data for creation is invalid");
            }

            return Ok(patient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, PatientDto patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Patient data for update is invalid");
            }

            return Ok(patient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patientToDelete = await _context.Patients.SingleOrDefaultAsync(p => p.Id == id);

            if (patientToDelete != null)
            {
                return Ok(patientToDelete);
            }

            return BadRequest($"There is no patient with id {id} to delete");
        }
    }
}
