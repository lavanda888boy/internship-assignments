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

        public Patient Add(Patient patient)
        {
            _context.Patients.Add(patient);
            return patient;
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await _context.Patients.AsNoTracking().ToListAsync();
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Patient>> SearchByPropertyAsync
            (Expression<Func<Patient, bool>> entityPredicate)
        {
            return await _context.Patients.AsNoTracking()
                                          .Where(entityPredicate)
                                          .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (patient is not null)
            {
                _context.Patients.Remove(patient);
            }
        }

        public async Task UpdateAsync(Patient patient)
        {
            var pat = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patient.Id);
            if (pat is not null)
            {
                _context.Update(pat);
            }
        }
    }
}
