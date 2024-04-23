using Hospital.Application.Abstractions;
using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hospital.Infrastructure.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HospitalManagementDbContext _context;

        public DepartmentRepository(HospitalManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Department> AddAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _context.Departments.AsNoTracking().ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Department>> SearchByPropertyAsync
            (Expression<Func<Department, bool>> entityPredicate)
        {
            return await _context.Departments.AsNoTracking()
                                             .Where(entityPredicate)
                                             .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            if (department is not null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Department department)
        {
            var dep = await _context.Departments.FirstOrDefaultAsync(d => d.Id == department.Id);
            if (dep is not null)
            {
                _context.Update(dep);
                await _context.SaveChangesAsync();
            }
        }
    }
}
