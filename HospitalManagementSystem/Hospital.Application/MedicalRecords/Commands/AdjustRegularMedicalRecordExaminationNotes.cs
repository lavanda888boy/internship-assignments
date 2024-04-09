using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record AdjustRegularMedicalRecordExaminationNotes(int Id, string ExaminationNotes) 
        : IRequest<RegularMedicalRecordDto>;

    public class AdjustRegularMedicalRecordExaminationNotesHandler 
        : IRequestHandler<AdjustRegularMedicalRecordExaminationNotes, RegularMedicalRecordDto>
    {
        private readonly IRegularMedicalRecordRepository _medicalRecordRepository;

        public AdjustRegularMedicalRecordExaminationNotesHandler(IRegularMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public Task<RegularMedicalRecordDto> Handle(AdjustRegularMedicalRecordExaminationNotes request, CancellationToken cancellationToken)
        {
            var existingRecord = _medicalRecordRepository.GetById(request.Id);
            if (existingRecord == null)
            {
                throw new NoEntityFoundException($"Cannot update non-existing regular medical record with id {request.Id}");
            }
            else
            {
                existingRecord.ExaminationNotes = request.ExaminationNotes;
                _medicalRecordRepository.Update(existingRecord);

                return Task.FromResult(RegularMedicalRecordDto.FromMedicalRecord(existingRecord));
            }
        }
    }
}
