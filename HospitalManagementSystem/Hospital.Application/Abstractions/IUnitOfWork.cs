namespace Hospital.Application.Abstractions
{
    public interface IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; }
        public IDiagnosisMedicalRecordRepository DiagnosisRecordRepository { get; }
        public IDoctorRepository DoctorRepository { get; }
        public IIllnessRepository IllnessRepository { get; }
        public IPatientRepository PatientRepository { get; }
        public IRegularMedicalRecordRepository RegularRecordRepository { get; }
        public ITreatmentRepository TreatmentRepository { get; }
        Task SaveAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
