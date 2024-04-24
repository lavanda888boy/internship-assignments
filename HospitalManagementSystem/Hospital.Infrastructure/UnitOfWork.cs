using Hospital.Application.Abstractions;
using Hospital.Domain.Models;

namespace Hospital.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HospitalManagementDbContext _context;
        public IRepository<Department> DepartmentRepository { get; private set; }
        public IRepository<DiagnosisMedicalRecord> DiagnosisRecordRepository { get; private set; }
        public IRepository<Doctor> DoctorRepository { get; private set; }
        public IRepository<Illness> IllnessRepository { get; private set; }
        public IRepository<Patient> PatientRepository { get; private set; }
        public IRepository<RegularMedicalRecord> RegularRecordRepository { get; private set; }
        public IRepository<Treatment> TreatmentRepository { get; private set; }

        public UnitOfWork(HospitalManagementDbContext context,
            IRepository<Department> departmentRepository,
            IRepository<DiagnosisMedicalRecord> diagnosisRecordRepository,
            IRepository<Doctor> doctorRepository,
            IRepository<Illness> illnessRepository,
            IRepository<Patient> patientRepository,
            IRepository<RegularMedicalRecord> regularRecordRepository,
            IRepository<Treatment> treatmentRepository)
        {
            _context = context;
            DepartmentRepository = departmentRepository;
            DiagnosisRecordRepository = diagnosisRecordRepository;
            DoctorRepository = doctorRepository;
            IllnessRepository = illnessRepository;
            PatientRepository = patientRepository;
            RegularRecordRepository = regularRecordRepository;
            TreatmentRepository = treatmentRepository;
        }

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
