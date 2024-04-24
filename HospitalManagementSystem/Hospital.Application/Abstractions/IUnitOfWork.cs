using Hospital.Domain.Models;

namespace Hospital.Application.Abstractions
{
    public interface IUnitOfWork
    {
        public IRepository<Department> DepartmentRepository { get; }
        public IRepository<DiagnosisMedicalRecord> DiagnosisRecordRepository { get; }
        public IRepository<Doctor> DoctorRepository { get; }
        public IRepository<Illness> IllnessRepository { get; }
        public IRepository<Patient> PatientRepository { get; }
        public IRepository<RegularMedicalRecord> RegularRecordRepository { get; }
        public IRepository<Treatment> TreatmentRepository { get; }
    }
}
