using Hospital.Application.Abstractions;
using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hospital.Infrastructure.Repository
{
    public class PatientRepository : IRepository<Patient>
    {
        private readonly HospitalManagementDbContext _context;

        public PatientRepository(HospitalManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Patient> AddAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await _context.Patients.AsNoTracking()
                                          .Include(p => p.DoctorsPatients)
                                          .ToListAsync();
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _context.Patients
                                 .Include(p => p.DoctorsPatients)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Patient>> SearchByPropertyAsync
            (Expression<Func<Patient, bool>> entityPredicate)
        {
            return await _context.Patients.AsNoTracking()
                                          .Include(p => p.DoctorsPatients)
                                          .Where(entityPredicate)
                                          .ToListAsync();
        }

        public async Task DeleteAsync(Patient patient)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Patient patient)
        {
            _context.Update(patient);
            await _context.SaveChangesAsync();
        }
    }
}
