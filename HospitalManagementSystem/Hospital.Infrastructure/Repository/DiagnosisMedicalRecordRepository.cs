﻿using Hospital.Application.Abstractions;
using Hospital.Application.Common;
using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Hospital.Infrastructure.Repository
{
    public class DiagnosisMedicalRecordRepository : IRepository<DiagnosisMedicalRecord>
    {
        private readonly HospitalManagementDbContext _context;

        public DiagnosisMedicalRecordRepository(HospitalManagementDbContext context)
        {
            _context = context;
        }

        public async Task<DiagnosisMedicalRecord> AddAsync(DiagnosisMedicalRecord record)
        {
            _context.DiagnosisRecords.Add(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<List<DiagnosisMedicalRecord>> GetAllAsync()
        {
            return await _context.DiagnosisRecords.AsNoTracking()
                                                  .Include(r => r.ExaminedPatient)
                                                  .Include(r => r.ResponsibleDoctor)
                                                  .ThenInclude(d => d.Department)
                                                  .Include(r => r.DiagnosedIllness)
                                                  .Include(r => r.ProposedTreatment)
                                                  .ToListAsync();
        }

        public async Task<PaginatedResult<DiagnosisMedicalRecord>> GetAllPaginatedAsync(int pageNumber, int pageSize)
        {
            var records = await _context.DiagnosisRecords.AsNoTracking()
                                                         .Include(r => r.ExaminedPatient)
                                                         .Include(r => r.ResponsibleDoctor)
                                                         .ThenInclude(d => d.Department)
                                                         .Include(r => r.DiagnosedIllness)
                                                         .Include(r => r.ProposedTreatment)
                                                         .Skip((pageNumber - 1) * pageSize)
                                                         .Take(pageSize)
                                                         .OrderByDescending(r => r.DateOfExamination)
                                                         .ToListAsync();

            return new PaginatedResult<DiagnosisMedicalRecord>
            {
                TotalItems = _context.DiagnosisRecords.Count(),
                Items = records
            };
        }

        public async Task<DiagnosisMedicalRecord?> GetByIdAsync(int id)
        {
            return await _context.DiagnosisRecords.Include(r => r.ExaminedPatient)
                                                  .Include(r => r.ResponsibleDoctor)
                                                  .ThenInclude(d => d.Department)
                                                  .Include(r => r.DiagnosedIllness)
                                                  .Include(r => r.ProposedTreatment)
                                                  .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<PaginatedResult<DiagnosisMedicalRecord>> SearchByPropertyPaginatedAsync
            (Expression<Func<DiagnosisMedicalRecord, bool>> entityPredicate, int pageNumber, int pageSize)
        {
            var records = _context.DiagnosisRecords.AsNoTracking()
                                                   .Include(r => r.ExaminedPatient)
                                                   .Include(r => r.ResponsibleDoctor)
                                                   .ThenInclude(d => d.Department)
                                                   .Include(r => r.DiagnosedIllness)
                                                   .Include(r => r.ProposedTreatment)
                                                   .Where(entityPredicate);

            var paginatedRecords = await records.Skip((pageNumber - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToListAsync();

            return new PaginatedResult<DiagnosisMedicalRecord>
            {
                TotalItems = records.Count(),
                Items = paginatedRecords
            };
        }

        public async Task DeleteAsync(DiagnosisMedicalRecord record)
        {
            _context.DiagnosisRecords.Remove(record);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DiagnosisMedicalRecord record)
        {
            _context.DiagnosisRecords.Update(record);
            await _context.SaveChangesAsync();
        }
    }
}
