using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record AdjustDiagnosisMedicalRecordExaminationNotes(int Id, string ExaminationNotes)
        : IRequest<DiagnosisMedicalRecordDto>;

    public class AdjustDiagnosisMedicalRecordExaminationNotesHandler
        : IRequestHandler<AdjustDiagnosisMedicalRecordExaminationNotes, DiagnosisMedicalRecordDto>
    {
        private readonly IRepository<DiagnosisMedicalRecord> _recordRepository;
        private readonly IMapper _mapper;

        public AdjustDiagnosisMedicalRecordExaminationNotesHandler(IRepository<DiagnosisMedicalRecord> recordRepository,
            IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        public async Task<DiagnosisMedicalRecordDto> Handle(AdjustDiagnosisMedicalRecordExaminationNotes request, CancellationToken cancellationToken)
        {
            var existingRecord = await _recordRepository.GetByIdAsync(request.Id);
            if (existingRecord == null)
            {
                throw new NoEntityFoundException($"Cannot update non-existing diagnosis medical record with id {request.Id}");
            }

            existingRecord.ExaminationNotes = request.ExaminationNotes;
            await _recordRepository.UpdateAsync(existingRecord);

            return await Task.FromResult(_mapper.Map<DiagnosisMedicalRecordDto>(existingRecord));
        }
    }
}
