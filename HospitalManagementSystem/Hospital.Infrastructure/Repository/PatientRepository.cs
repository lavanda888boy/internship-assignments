using Hospital.Application.Abstractions;
using Hospital.Application.Common;
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
                                          .ThenInclude(dp => dp.Doctor)
                                          .ThenInclude(d => d.Department)
                                          .ToListAsync();
        }

        public async Task<PaginatedResult<Patient>> GetAllPaginatedAsync(int pageNumber, int pageSize)
        {
            var patients= await _context.Patients.AsNoTracking()
                                                 .Include(p => p.DoctorsPatients)
                                                 .ThenInclude(dp => dp.Doctor)
                                                 .ThenInclude(d => d.Department)
                                                 .Skip((pageNumber - 1) * pageSize)
                                                 .Take(pageSize)
                                                 .ToListAsync();

            return new PaginatedResult<Patient>
            {
                TotalItems = _context.Patients.Count(),
                Items = patients
            };
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _context.Patients.Include(p => p.DoctorsPatients)
                                          .ThenInclude(dp => dp.Doctor)
                                          .ThenInclude(d => d.Department)
                                          .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PaginatedResult<Patient>> SearchByPropertyPaginatedAsync
            (Expression<Func<Patient, bool>> entityPredicate, int pageNumber, int pageSize)
        {
            var patients = _context.Patients.AsNoTracking()
                                            .Include(p => p.DoctorsPatients)
                                            .ThenInclude(dp => dp.Doctor)
                                            .ThenInclude(d => d.Department)
                                            .Where(entityPredicate);

            var paginatedPatients = await patients.Skip((pageNumber - 1) * pageSize)
                                                  .Take(pageSize)
                                                  .ToListAsync();

            return new PaginatedResult<Patient>
            {
                TotalItems = patients.Count(),
                Items = paginatedPatients
            };
        }

        public async Task DeleteAsync(Patient patient)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }
    }
}
