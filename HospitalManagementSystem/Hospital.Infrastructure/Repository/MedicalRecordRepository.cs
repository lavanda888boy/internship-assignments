using Hospital.Application.Abstractions;
using Hospital.Domain.Models;

namespace Hospital.Infrastructure.Repository
{
    internal class MedicalRecordRepository : IRepository<MedicalRecord>
    {
        private List<MedicalRecord> _medicalRecords = new();

        public MedicalRecord Create(MedicalRecord record)
        {
            _medicalRecords.Add(record);
            return record;
        }

        public bool Delete(MedicalRecord record)
        {
            return _medicalRecords.Remove(record);
        }

        public List<MedicalRecord> GetAll()
        {
            return _medicalRecords;
        }

        public MedicalRecord GetById(int id)
        {
            return _medicalRecords.Single(mr => mr.Id == id);
        }

        public List<MedicalRecord> GetByProperty(Func<MedicalRecord, bool> medicalRecordProperty)
        {
            return _medicalRecords.Where(medicalRecordProperty).ToList();
        }

        public int GetLastId()
        {
            return _medicalRecords.Any() ? _medicalRecords.Max(record => record.Id) : 0;
        }

        public MedicalRecord? Update(MedicalRecord record)
        {
            var existingRecord = _medicalRecords.FirstOrDefault(r => r.Id == record.Id);
            if (existingRecord != null)
            {
                existingRecord.ExaminedPatient = record.ExaminedPatient;
                existingRecord.ResponsibleDoctor = record.ResponsibleDoctor;
                existingRecord.DateOfExamination = record.DateOfExamination;
                existingRecord.ExaminationNotes = record.ExaminationNotes;
                existingRecord.Diagnosis = record.Diagnosis;
            }
            return existingRecord;
        }
    }
}
