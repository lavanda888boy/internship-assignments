using Hospital.Domain.Models;
using Hospital.Infrastructure;
using Hospital.Presentation.Dto.Patient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        [HttpPost]
        public async Task<IActionResult> AddPatient(PatientDto patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Patient data for creation is invalid");
            }

            return Ok(patient);
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchPatientsByASetOfProperties(PatientFilterDto patientFilter)
        {
            

            return Ok();
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
