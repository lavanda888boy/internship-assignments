using Hospital.Application.Abstractions;
using Hospital.Domain.Models;

namespace Hospital.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Department> DepartmentRepository { get; private set; }
        public IRepository<DiagnosisMedicalRecord> DiagnosisRecordRepository { get; private set; }
        public IRepository<Doctor> DoctorRepository { get; private set; }
        public IRepository<Illness> IllnessRepository { get; private set; }
        public IRepository<Patient> PatientRepository { get; private set; }
        public IRepository<RegularMedicalRecord> RegularRecordRepository { get; private set; }
        public IRepository<Treatment> TreatmentRepository { get; private set; }

        public UnitOfWork(IRepository<Department> departmentRepository,
            IRepository<DiagnosisMedicalRecord> diagnosisRecordRepository,
            IRepository<Doctor> doctorRepository,
            IRepository<Illness> illnessRepository,
            IRepository<Patient> patientRepository,
            IRepository<RegularMedicalRecord> regularRecordRepository,
            IRepository<Treatment> treatmentRepository)
        {
            DepartmentRepository = departmentRepository;
            DiagnosisRecordRepository = diagnosisRecordRepository;
            DoctorRepository = doctorRepository;
            IllnessRepository = illnessRepository;
            PatientRepository = patientRepository;
            RegularRecordRepository = regularRecordRepository;
            TreatmentRepository = treatmentRepository;
        }
    }
}
