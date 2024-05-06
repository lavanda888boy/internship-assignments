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

        public async Task<RegularMedicalRecord> AddAsync(RegularMedicalRecord record)
        {
            _context.RegularRecords.Add(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<List<RegularMedicalRecord>> GetAllAsync()
        {
            return await _context.RegularRecords.AsNoTracking()
                                                .Include(r => r.ExaminedPatient)
                                                .Include(r => r.ResponsibleDoctor)
                                                .ThenInclude(d => d.Department)
                                                .ToListAsync();
        }

        public async Task<RegularMedicalRecord?> GetByIdAsync(int id)
        {
            return await _context.RegularRecords.AsNoTracking()
                                                .Include(r => r.ExaminedPatient)
                                                .Include(r => r.ResponsibleDoctor)
                                                .ThenInclude(d => d.Department)
                                                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<RegularMedicalRecord>> SearchByPropertyAsync
            (Expression<Func<RegularMedicalRecord, bool>> entityPredicate)
        {
            return await _context.RegularRecords.AsNoTracking()
                                                .Include(r => r.ExaminedPatient)
                                                .Include(r => r.ResponsibleDoctor)
                                                .ThenInclude(d => d.Department)
                                                .Where(entityPredicate)
                                                .ToListAsync();
        }

        public async Task DeleteAsync(RegularMedicalRecord record)
        {
            _context.RegularRecords.Remove(record);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RegularMedicalRecord record)
        {
            _context.Update(record);
            await _context.SaveChangesAsync();
        }
    }
}
