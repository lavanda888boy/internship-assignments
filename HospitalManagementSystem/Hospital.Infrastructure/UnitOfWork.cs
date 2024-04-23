using Hospital.Application.Abstractions;

namespace Hospital.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HospitalManagementDbContext _context;
        public IDepartmentRepository DepartmentRepository { get; private set; }
        public IDiagnosisMedicalRecordRepository DiagnosisRecordRepository { get; private set; }
        public IDoctorRepository DoctorRepository { get; private set; }
        public IIllnessRepository IllnessRepository { get; private set; }
        public IPatientRepository PatientRepository { get; private set; }
        public IRegularMedicalRecordRepository RegularRecordRepository { get; private set; }
        public ITreatmentRepository TreatmentRepository { get; private set; }

        public UnitOfWork(HospitalManagementDbContext context,
            IDepartmentRepository departmentRepository,
            IDiagnosisMedicalRecordRepository diagnosisRecordRepository,
            IDoctorRepository doctorRepository,
            IIllnessRepository illnessRepository,
            IPatientRepository patientRepository,
            IRegularMedicalRecordRepository regularRecordRepository,
            ITreatmentRepository treatmentRepository)
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
