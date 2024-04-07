using Hospital.Application.Abstractions;
using Hospital.Domain.Models;

namespace Hospital.Infrastructure.Repository
{
    internal class MedicalRecordRepository : IRepository<RegularMedicalRecord>
    {
        private List<RegularMedicalRecord> _medicalRecords = new();

        public RegularMedicalRecord Create(RegularMedicalRecord record)
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

        public List<RegularMedicalRecord> GetAll()
        {
            return _medicalRecords;
        }

        public RegularMedicalRecord GetById(int id)
        {
            return _medicalRecords.First(mr => mr.Id == id);
        }

        public List<RegularMedicalRecord> SearchByProperty(Func<RegularMedicalRecord, bool> medicalRecordProperty)
        {
            return _medicalRecords.Where(medicalRecordProperty).ToList();
        }

        public int GetLastId()
        {
            return _medicalRecords.Any() ? _medicalRecords.Max(record => record.Id) : 0;
        }

        public bool Update(RegularMedicalRecord record)
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
