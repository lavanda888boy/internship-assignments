using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record UpdateRegularMedicalRecord(int Id, int PatientId, int DoctorId,
       DateTimeOffset DateOfExamination, string ExaminationNotes) : IRequest<RegularMedicalRecordDto>;

    public class UpdateRegularMedicalRecordHandler 
        : IRequestHandler<UpdateRegularMedicalRecord, RegularMedicalRecordDto>
    {
        private readonly IRegularMedicalRecordRepository _medicalRecordRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;

        public UpdateRegularMedicalRecordHandler(IRegularMedicalRecordRepository medicalRecordRepository,
            IPatientRepository patientRepository, IDoctorRepository doctorRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }

        public Task<RegularMedicalRecordDto> Handle(UpdateRegularMedicalRecord request, CancellationToken cancellationToken)
        {
            var result = _medicalRecordRepository.Update(new RegularMedicalRecord()
            {
                Id = request.Id,
                ExaminedPatient = _patientRepository.GetById(request.PatientId),
                ResponsibleDoctor = _doctorRepository.GetById(request.DoctorId),
                DateOfExamination = request.DateOfExamination,
                ExaminationNotes = request.ExaminationNotes
            });

            if (result)
            {
                var updatedRecord = _medicalRecordRepository.GetById(request.Id);
                return Task.FromResult(RegularMedicalRecordDto.FromMedicalRecord(updatedRecord));
            }
            else
            {
                throw new NoEntityFoundException($"Cannot update non-existing regular medical record with id {request.Id}");
            }
        }
    }
}
