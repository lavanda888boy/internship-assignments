using Hospital.Application.Abstractions;
using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hospital.Infrastructure.Repository
{
    public class RegularMedicalRecordRepository : IRepository<RegularMedicalRecord>
    {
        private readonly HospitalManagementDbContext _context;

        public RegularMedicalRecordRepository(HospitalManagementDbContext context)
        {
            _context = context;
        }

        public RegularMedicalRecord Add(RegularMedicalRecord record)
        {
            _context.RegularRecords.Add(record);
            return record;
        }

        public async Task<List<RegularMedicalRecord>> GetAllAsync()
        {
            return await _context.RegularRecords.AsNoTracking().ToListAsync();
        }

        public async Task<RegularMedicalRecord?> GetByIdAsync(int id)
        {
            return await _context.RegularRecords.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<RegularMedicalRecord>> SearchByPropertyAsync
            (Expression<Func<RegularMedicalRecord, bool>> entityPredicate)
        {
            return await _context.RegularRecords.AsNoTracking()
                                                .Where(entityPredicate)
                                                .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var record = await _context.RegularRecords.FirstOrDefaultAsync(r => r.Id == id);
            if (record is not null)
            {
                _context.RegularRecords.Remove(record);
            }
        }

        public async Task UpdateAsync(RegularMedicalRecord record)
        {
            var rec = await _context.RegularRecords.FirstOrDefaultAsync(r => r.Id == record.Id);
            if (rec is not null)
            {
                _context.Update(rec);
            }
        }
    }
}
