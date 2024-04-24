using Hospital.Application.Abstractions;
using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hospital.Infrastructure.Repository
{
    public class DoctorRepository : IRepository<Doctor>
    {
        private readonly HospitalManagementDbContext _context;

        public DoctorRepository(HospitalManagementDbContext context)
        {
            _context = context;
        }

        public Doctor Add(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            return doctor;
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.AsNoTracking().ToListAsync();
        }

        public async Task<Doctor?> GetByIdAsync(int id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Doctor>> SearchByPropertyAsync
            (Expression<Func<Doctor, bool>> entityPredicate)
        {
            return await _context.Doctors.AsNoTracking()
                                         .Where(entityPredicate)
                                         .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            if (doctor is not null)
            {
                _context.Doctors.Remove(doctor);
            }
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            var doc = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctor.Id);
            if (doc is not null)
            {
                _context.Update(doc);
            }
        }
    }
}
