using Hospital.Domain.Models;
using Hospital.Infrastructure;
using Hospital.Presentation.Dto.Record;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegularMedicalRecordController : ControllerBase
    {
        private readonly HospitalManagementDbContext _context = new HospitalManagementDbContext();

        [HttpGet]
        public async Task<IActionResult> GetAllRegularMedicalRecords()
        {
            var records = await _context.RegularRecords.Select(r => new
            {
                Id = r.Id,
                Patient = r.ExaminedPatient,
                Doctor = r.ResponsibleDoctor,
                Notes = r.ExaminationNotes,
            }).ToListAsync();

            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegularMedicalRecordById(int id)
        {
            var record = await _context.RegularRecords.SingleOrDefaultAsync(r => r.Id == id);
            return Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegularMedicalRecord(RegularMedicalRecordDto record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Regular record data for creation is invalid");
            }

            return Ok(record);
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchRegularMedicalRecordsByASetOfProperties(RegularMedicalRecordFilterDto recordFilter)
        {
            Expression<Func<RegularMedicalRecord, bool>> predicate = r =>
                (recordFilter.ExaminedPatientId == 0 || r.ExaminedPatient.Id == recordFilter.ExaminedPatientId) &&
                (recordFilter.ResponsibleDoctorId == 0 || r.ResponsibleDoctor.Id == recordFilter.ResponsibleDoctorId) &&
                (!recordFilter.DateOfExamination.HasValue || r.DateOfExamination == recordFilter.DateOfExamination);

            var records = _context.RegularRecords.Where(predicate.Compile());

            return Ok(records);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegularMedicalRecord(int id, RegularMedicalRecordDto record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Regular record data for update is invalid");
            }

            return Ok(record);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegularMedicalRecord(int id)
        {
            var recordToDelete = await _context.RegularRecords.SingleOrDefaultAsync(r => r.Id == id);

            if (recordToDelete != null)
            {
                return Ok(recordToDelete);
            }

            return BadRequest($"There is no regular record with id {id} to delete");
        }
    }
}
