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
    public class DiagnosisMedicalRecordController : ControllerBase
    {
        private readonly HospitalManagementDbContext _context = new HospitalManagementDbContext();

        [HttpGet]
        public async Task<IActionResult> GetAllDiagnosisMedicalRecords()
        {
            var records = await _context.DiagnosisRecords.Select(r => new
            {
                Id = r.Id,
                Patient = r.ExaminedPatient,
                Doctor = r.ResponsibleDoctor,
                Notes = r.ExaminationNotes,
                Illness = r.DiagnosedIllness.Name,
                Treatment = r.ProposedTreatment.PrescribedMedicine,
            }).ToListAsync();

            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiagnosisMedicalRecordById(int id)
        {
            var record = await _context.DiagnosisRecords.SingleOrDefaultAsync(r => r.Id == id);
            return Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult> AddDiagnosisMedicalRecord(DiagnosisMedicalRecordDto record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Diagnosis record data for creation is invalid");
            }

            return Ok(record);
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchDiagnosisMedicalRecordsByASetOfProperties(DiagnosisMedicalRecordFilterDto recordFilter)
        {
            Expression<Func<DiagnosisMedicalRecord, bool>> predicate = r =>
                (recordFilter.ExaminedPatientId == 0 || r.ExaminedPatient.Id == recordFilter.ExaminedPatientId) &&
                (recordFilter.ResponsibleDoctorId == 0 || r.ResponsibleDoctor.Id == recordFilter.ResponsibleDoctorId) &&
                (!recordFilter.DateOfExamination.HasValue || r.DateOfExamination == recordFilter.DateOfExamination) &&
                (string.IsNullOrEmpty(recordFilter.DiagnosedIllnessName) || r.DiagnosedIllness.Name == recordFilter.DiagnosedIllnessName) &&
                (string.IsNullOrEmpty(recordFilter.PrescribedMedicine) || r.ProposedTreatment.PrescribedMedicine == recordFilter.PrescribedMedicine);

            var records = _context.DiagnosisRecords.Where(predicate.Compile());

            return Ok(records);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiagnosisMedicalRecord(int id, DiagnosisMedicalRecordDto record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Diagnosis record data for update is invalid");
            }

            return Ok(record);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnosisMedicalRecord(int id)
        {
            var recordToDelete = await _context.DiagnosisRecords.SingleOrDefaultAsync(r => r.Id == id);

            if (recordToDelete != null)
            {
                return Ok(recordToDelete);
            }

            return BadRequest($"There is no diagnosis record with id {id} to delete");
        }
    }
}
