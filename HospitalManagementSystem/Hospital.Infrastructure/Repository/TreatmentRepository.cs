using Hospital.Application.Abstractions;
using Hospital.Application.Common;
using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Linq.Expressions;

namespace Hospital.Infrastructure.Repository
{
    public class TreatmentRepository : IRepository<Treatment>
    {
        private readonly HospitalManagementDbContext _context;

        public TreatmentRepository(HospitalManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Treatment> AddAsync(Treatment treatment)
        {
            _context.Treatments.Add(treatment);
            await _context.SaveChangesAsync();
            return treatment;
        }

        public async Task<List<Treatment>> GetAllAsync()
        {
            return await _context.Treatments.AsNoTracking().ToListAsync();
        }

        public async Task<Treatment?> GetByIdAsync(int id)
        {
            return await _context.Treatments.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<PaginatedResult<Treatment>> SearchByPropertyPaginatedAsync
            (Expression<Func<Treatment, bool>> entityPredicate, int pageNumber, int pageSize)
        {
            var treatments = _context.Treatments.AsNoTracking()
                                                .Where(entityPredicate)
                                                .AsQueryable();

            var paginatedTreatments = await treatments.Skip((pageNumber - 1) * pageSize)
                                                      .Take(pageSize)
                                                      .ToListAsync();

            return new PaginatedResult<Treatment>
            {
                TotalItems = treatments.Count(),
                Items = paginatedTreatments
            };
        }

        public async Task DeleteAsync(Treatment treatment)
        {
            _context.Treatments.Remove(treatment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Treatment treatment)
        {
            _context.Treatments.Update(treatment);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedResult<Treatment>> GetAllPaginatedAsync(int pageNumber, int pageSize)
        {
            var treatments = await _context.Treatments.AsNoTracking()
                                                      .Skip((pageNumber - 1) * pageSize)
                                                      .Take(pageSize)
                                                      .ToListAsync();

            return new PaginatedResult<Treatment>
            {
                TotalItems = _context.Treatments.Count(),
                Items = treatments
            };
        }
    }
}
