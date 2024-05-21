using Hospital.Application.Abstractions;
using Hospital.Application.Common;
using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hospital.Infrastructure.Repository
{
    public class DepartmentRepository : IRepository<Department>
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

        public async Task<PaginatedResult<Department>> SearchByPropertyPaginatedAsync
            (Expression<Func<Department, bool>> entityPredicate, int pageNumber, int pageSize)
        {
            var departments = _context.Departments.AsNoTracking()
                                                  .Where(entityPredicate);

            var paginatedDepartments = await departments.Skip((pageNumber - 1) * pageSize)
                                                        .Take(pageSize)
                                                        .ToListAsync();

            return new PaginatedResult<Department>
            {
                TotalItems = departments.Count(),
                Items = paginatedDepartments
            };
        }

        public async Task DeleteAsync(Department department)
        {
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedResult<Department>> GetAllPaginatedAsync(int pageNumber, int pageSize)
        {
            var departments = await _context.Departments.AsNoTracking()
                                                        .Skip((pageNumber - 1) * pageSize)
                                                        .Take(pageSize)
                                                        .ToListAsync();

            return new PaginatedResult<Department>
            {
                TotalItems = _context.Departments.Count(),
                Items = departments
            };
        }
    }
}
