using Hospital.Application.Abstractions;
using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
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

        public Treatment Add(Treatment treatment)
        {
            _context.Treatments.Add(treatment);
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

        public async Task<List<Treatment>> SearchByPropertyAsync
            (Expression<Func<Treatment, bool>> entityPredicate)
        {
            return await _context.Treatments.AsNoTracking()
                                            .Where(entityPredicate)
                                            .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var treatment = await _context.Treatments.FirstOrDefaultAsync(t => t.Id == id);
            if (treatment is not null)
            {
                _context.Treatments.Remove(treatment);
            }
        }

        public async Task UpdateAsync(Treatment treatment)
        {
            var treat = await _context.Treatments.FirstOrDefaultAsync(t => t.Id == treatment.Id);
            if (treat is not null)
            {
                _context.Update(treat);
            }
        }
    }
}
