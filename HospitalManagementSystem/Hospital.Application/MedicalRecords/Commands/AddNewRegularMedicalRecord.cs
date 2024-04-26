using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record AddNewRegularMedicalRecord(int PatientId, int DoctorId,
        string ExaminationNotes) : IRequest<RegularMedicalRecordDto>;

    public class AddNewRegularMedicalRecordHandler
        : IRequestHandler<AddNewRegularMedicalRecord, RegularMedicalRecordDto>
    {
        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<RegularMedicalRecord> _recordRepository;

        public AddNewRegularMedicalRecordHandler(IRepository<Patient> patientRepository, 
            IRepository<Doctor> doctorRepository,
            IRepository<RegularMedicalRecord> recordRepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _recordRepository = recordRepository;
        }

        public async Task<RegularMedicalRecordDto> Handle(AddNewRegularMedicalRecord request,
            CancellationToken cancellationToken)
        {
            var examinedPatient = await _patientRepository.GetByIdAsync(request.PatientId);
            var responsibleDoctor = await _doctorRepository.GetByIdAsync(request.DoctorId);

            if (examinedPatient == null)
            {
                throw new NoEntityFoundException($"Cannot create regular medical record of a non-existing patient with id {request.PatientId}");
            }

            if (responsibleDoctor == null)
            {
                throw new NoEntityFoundException($"Cannot create regular medical record for a non-existing doctor with id {request.DoctorId}");
            }

            bool examinedPatientIsAssignedToTheDoctor = examinedPatient.DoctorsPatients
                                                                        .Any(dp => dp.DoctorId == responsibleDoctor.Id);
            if (examinedPatientIsAssignedToTheDoctor)
            {
                var medicalRecord = new RegularMedicalRecord
                {
                    ExaminedPatient = examinedPatient,
                    ResponsibleDoctor = responsibleDoctor,
                    DateOfExamination = DateTimeOffset.UtcNow,
                    ExaminationNotes = request.ExaminationNotes
                };

                var createdRecord = await _recordRepository.AddAsync(medicalRecord);

                return await Task.FromResult(RegularMedicalRecordDto.FromMedicalRecord(createdRecord));
            }
            else
            {
                throw new PatientDoctorMisassignationException("Cannot create regular medical record beacuse the patient is not assigned to the doctor");
            }
        }
    }
}
