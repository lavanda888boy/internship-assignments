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

        public bool Delete(RegularMedicalRecord record)
        {
            return _medicalRecords.Remove(record);
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
            var existingRecord = _medicalRecords.FirstOrDefault(r => r.Id == record.Id);
            if (existingRecord != null)
            {
                existingRecord.ExaminedPatient = record.ExaminedPatient;
                existingRecord.ResponsibleDoctor = record.ResponsibleDoctor;
                existingRecord.DateOfExamination = record.DateOfExamination;
                existingRecord.ExaminationNotes = record.ExaminationNotes;

                if (record is DiagnosisMedicalRecord)
                {
                    ((DiagnosisMedicalRecord)existingRecord).DiagnosedIllness = ((DiagnosisMedicalRecord)record).DiagnosedIllness;
                    ((DiagnosisMedicalRecord)existingRecord).ProposedTreatment = ((DiagnosisMedicalRecord)record).ProposedTreatment;
                }

                return true;
            }
            return false;
        }
    }
}
