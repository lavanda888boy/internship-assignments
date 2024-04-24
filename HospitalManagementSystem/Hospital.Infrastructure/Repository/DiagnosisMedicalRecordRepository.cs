using Hospital.Application.Abstractions;
using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hospital.Infrastructure.Repository
{
    public class DiagnosisMedicalRecordRepository : IRepository<DiagnosisMedicalRecord>
    {
        private readonly HospitalManagementDbContext _context;

        public DiagnosisMedicalRecordRepository(HospitalManagementDbContext context)
        {
            _context = context;
        }

        public DiagnosisMedicalRecord Add(DiagnosisMedicalRecord record)
        {
            _context.DiagnosisRecords.Add(record);
            return record;
        }

        public async Task<List<DiagnosisMedicalRecord>> GetAllAsync()
        {
            return await _context.DiagnosisRecords.AsNoTracking().ToListAsync();
        }

        public async Task<DiagnosisMedicalRecord?> GetByIdAsync(int id)
        {
            return await _context.DiagnosisRecords.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<DiagnosisMedicalRecord>> SearchByPropertyAsync
            (Expression<Func<DiagnosisMedicalRecord, bool>> entityPredicate)
        {
            return await _context.DiagnosisRecords.AsNoTracking()
                                                  .Where(entityPredicate)
                                                  .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var record = await _context.DiagnosisRecords.FirstOrDefaultAsync(r => r.Id == id);
            if (record is not null)
            {
                _context.DiagnosisRecords.Remove(record);
            }
        }

        public async Task UpdateAsync(DiagnosisMedicalRecord record)
        {
            var rec = await _context.DiagnosisRecords.FirstOrDefaultAsync(r => r.Id == record.Id);
            if (rec is not null)
            {
                _context.Update(rec);
            }
        }
    }
}
