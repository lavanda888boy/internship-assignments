using Hospital.Domain.Models;
using Hospital.Infrastructure;
using Hospital.Presentation.Dto.Doctor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly HospitalManagementDbContext _context = new HospitalManagementDbContext();

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _context.Doctors.Select(d => new
            {
                Id = d.Id,
                Name = d.Name,
                Surname = d.Surname,
                Department = d.Department.Name,
                Doctors = d.DoctorsPatients.Select(dp => dp.Patient.Name)
                                           .ToList()
            }).ToListAsync();

            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _context.Doctors.SingleOrDefaultAsync(d => d.Id == id);
            return Ok(doctor);
        }

        //[HttpPost]
        //public async Task<IActionResult> SearchDoctorsByASetOfProperties(DoctorFilterDto doctorFilter)
        //{
        //    Expression<Func<Doctor, bool>> predicate = d =>
        //        (string.IsNullOrEmpty(doctorFilter.Name) || d.Name == doctorFilter.Name) &&
        //        (string.IsNullOrEmpty(doctorFilter.Surname) || d.Surname == doctorFilter.Surname) &&
        //        (string.IsNullOrEmpty(doctorFilter.Address) || d.Address == doctorFilter.Address) &&
        //        (string.IsNullOrEmpty(doctorFilter.PhoneNumber) || d.PhoneNumber == doctorFilter.PhoneNumber) &&
        //        (string.IsNullOrEmpty(doctorFilter.DepartmentName) || d.Department.Name == doctorFilter.DepartmentName);

        //    var doctors = _context.Doctors.Where(predicate.Compile());

        //    return Ok();
        //}

        [HttpPost]
        public async Task<IActionResult> AddDoctor(DoctorDto doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Doctor data for creation is invalid");
            }

            return Ok(doctor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, DoctorDto doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Doctor data for update is invalid");
            }

            return Ok(doctor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctorToDelete = await _context.Doctors.SingleOrDefaultAsync(d => d.Id == id);

            if (doctorToDelete != null)
            {
                return Ok(doctorToDelete);
            }

            return BadRequest($"There is no doctor with id {id} to delete");
        }
    }
}
