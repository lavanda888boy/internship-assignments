using Hospital.Application.Abstractions;
using Hospital.Domain.Models;

namespace Hospital.Infrastructure.Repository
{
    public class DiagnosisMedicalRecordRepository : IDiagnosisMedicalRecordRepository
    {
        private List<DiagnosisMedicalRecord> _medicalRecords = new();

        public DiagnosisMedicalRecord Create(DiagnosisMedicalRecord record)
        {
            _medicalRecords.Add(record);
            return record;
        }

        public bool Delete(int recordId)
        {
            var recordToRemove = GetById(recordId);
            if (recordToRemove is null)
            {
                return false;
            }

            _medicalRecords.Remove(recordToRemove);
            return true;
        }

        public List<DiagnosisMedicalRecord> GetAll()
        {
            return _medicalRecords;
        }

        public DiagnosisMedicalRecord? GetById(int id)
        {
            return _medicalRecords.FirstOrDefault(mr => mr.Id == id);
        }

        public List<DiagnosisMedicalRecord> SearchByProperty(Func<DiagnosisMedicalRecord, bool> medicalRecordProperty)
        {
            return _medicalRecords.Where(medicalRecordProperty).ToList();
        }

        public bool Update(DiagnosisMedicalRecord record)
        {
            var existingRecord = GetById(record.Id);
            if (existingRecord != null)
            {
                int index = _medicalRecords.IndexOf(existingRecord);
                _medicalRecords[index] = record;
                return true;
            }
            return false;
        }
    }
}
