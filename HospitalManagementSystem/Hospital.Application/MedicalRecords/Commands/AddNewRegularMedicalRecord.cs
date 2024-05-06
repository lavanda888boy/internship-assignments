using AutoMapper;
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
        private readonly IMapper _mapper;

        public AddNewRegularMedicalRecordHandler(IRepository<Patient> patientRepository, 
            IRepository<Doctor> doctorRepository,
            IRepository<RegularMedicalRecord> recordRepository,
            IMapper mapper)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _recordRepository = recordRepository;
            _mapper = mapper;
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
                DateTimeOffset now = DateTimeOffset.UtcNow;

                var medicalRecord = new RegularMedicalRecord
                {
                    ExaminedPatient = examinedPatient,
                    ResponsibleDoctor = responsibleDoctor,
                    DateOfExamination = new DateTimeOffset(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, now.Offset),
                    ExaminationNotes = request.ExaminationNotes
                };

                var createdRecord = await _recordRepository.AddAsync(medicalRecord);

                return await Task.FromResult(_mapper.Map<RegularMedicalRecordDto>(createdRecord));
            }
            else
            {
                throw new PatientDoctorMisassignationException("Cannot create regular medical record because the patient is not assigned to the doctor");
            }
        }
    }
}
