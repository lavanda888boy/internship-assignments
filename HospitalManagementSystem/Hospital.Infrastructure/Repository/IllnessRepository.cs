using Hospital.Application.Abstractions;
using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hospital.Infrastructure.Repository
{
    public class IllnessRepository : IRepository<Illness>
    {
        private readonly HospitalManagementDbContext _context;

        public IllnessRepository(HospitalManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Illness> AddAsync(Illness illness)
        {
            _context.Illnesses.Add(illness);
            await _context.SaveChangesAsync();
            return illness;
        }

        public async Task<List<Illness>> GetAllAsync()
        {
            return await _context.Illnesses.AsNoTracking().ToListAsync();
        }

        public async Task<Illness?> GetByIdAsync(int id)
        {
            return await _context.Illnesses.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Illness>> SearchByPropertyAsync
            (Expression<Func<Illness, bool>> entityPredicate)
        {
            return await _context.Illnesses.AsNoTracking()
                                           .Where(entityPredicate)
                                           .ToListAsync();
        }

        public async Task DeleteAsync(Illness illness)
        {
            _context.Illnesses.Remove(illness);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Illness illness)
        {
            _context.Update(illness);
            await _context.SaveChangesAsync();
        }
    }
}
