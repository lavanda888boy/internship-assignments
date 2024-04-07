using Hospital.Application.Abstractions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record CreateRegularMedicalRecord(int Id, int PatientId, int DoctorId,
       DateTimeOffset DateOfExamination, string ExaminationNotes) : IRequest<RegularMedicalRecordDto>;

    public class CreateRegularMedicalRecordHandler 
        : IRequestHandler<CreateRegularMedicalRecord, RegularMedicalRecordDto>
    {
        private readonly IRegularMedicalRecordRepository _medicalRecordRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;

        public CreateRegularMedicalRecordHandler(IRegularMedicalRecordRepository medicalRecordRepository,
            IPatientRepository patientRepository, IDoctorRepository doctorRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }

        public Task<RegularMedicalRecordDto> Handle(CreateRegularMedicalRecord request,
            CancellationToken cancellationToken)
        {
            var medicalRecord = new RegularMedicalRecord
            {
                Id = request.Id,
                ExaminedPatient = _patientRepository.GetById(request.PatientId),
                ResponsibleDoctor = _doctorRepository.GetById(request.DoctorId),
                DateOfExamination = request.DateOfExamination,
                ExaminationNotes = request.ExaminationNotes
            };

            var createdRecord = _medicalRecordRepository.Create(medicalRecord);
            return Task.FromResult(RegularMedicalRecordDto.FromMedicalRecord(createdRecord));
        }
    }
}
