using Hospital.Application.Abstractions;
using Hospital.Application.Common;
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

        public async Task<Doctor> AddAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.AsNoTracking()
                                         .Include(d => d.DoctorsPatients)
                                         .ThenInclude(dp => dp.Patient)
                                         .Include(d => d.Department)
                                         .Include(d => d.WorkingHours)
                                         .ThenInclude(wh => wh.DoctorScheduleWeekDay)
                                         .ThenInclude(dsw => dsw.WeekDay)
                                         .ToListAsync();
        }

        public async Task<PaginatedResult<Doctor>> GetAllPaginatedAsync(int pageNumber, int pageSize)
        {
            var doctors = await _context.Doctors.AsNoTracking()
                                                .Include(d => d.DoctorsPatients)
                                                .ThenInclude(dp => dp.Patient)
                                                .Include(d => d.Department)
                                                .Include(d => d.WorkingHours)
                                                .ThenInclude(wh => wh.DoctorScheduleWeekDay)
                                                .ThenInclude(dsw => dsw.WeekDay)
                                                .Skip((pageNumber - 1) * pageSize)
                                                .Take(pageSize)
                                                .OrderBy(d => d.Department)
                                                .ToListAsync();

            return new PaginatedResult<Doctor>
            {
                TotalItems = _context.Doctors.Count(),
                Items = doctors
            };
        }

        public async Task<Doctor?> GetByIdAsync(int id)
        {
            return await _context.Doctors.Include(d => d.DoctorsPatients)
                                         .ThenInclude(dp => dp.Patient)
                                         .Include(d => d.Department)
                                         .Include(d => d.WorkingHours)
                                         .ThenInclude(wh => wh.DoctorScheduleWeekDay)
                                         .ThenInclude(dsw => dsw.WeekDay)
                                         .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<PaginatedResult<Doctor>> SearchByPropertyPaginatedAsync
            (Expression<Func<Doctor, bool>> entityPredicate, int pageNumber, int pageSize)
        {
            var doctors = _context.Doctors.AsNoTracking()
                                          .Include(d => d.DoctorsPatients)
                                          .ThenInclude(dp => dp.Patient)
                                          .Include(d => d.Department)
                                          .Include(d => d.WorkingHours)
                                          .ThenInclude(wh => wh.DoctorScheduleWeekDay)
                                          .ThenInclude(dsw => dsw.WeekDay)
                                          .Where(entityPredicate);

            var paginatedDoctors = await doctors.Skip((pageNumber - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToListAsync();

            return new PaginatedResult<Doctor>
            {
                TotalItems = doctors.Count(),
                Items = paginatedDoctors
            };
        }

        public async Task DeleteAsync(Doctor doctor)
        {
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }
    }
}
