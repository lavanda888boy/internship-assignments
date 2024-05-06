using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record RemoveWronglyAddedDiagnosisMedicalRecord(int RecordId) : IRequest<DiagnosisMedicalRecordDto>;

    public class RemoveWronglyAddedDiagnosisMedicalRecordHandler :
        IRequestHandler<RemoveWronglyAddedDiagnosisMedicalRecord, DiagnosisMedicalRecordDto>
    {
        private readonly IRepository<DiagnosisMedicalRecord> _recordRepository;
        private readonly IMapper _mapper;

        public RemoveWronglyAddedDiagnosisMedicalRecordHandler(IRepository<DiagnosisMedicalRecord> recordRepository,
            IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        public async Task<DiagnosisMedicalRecordDto> Handle(RemoveWronglyAddedDiagnosisMedicalRecord request,
            CancellationToken cancellationToken)
        {
            var recordToDelete = await _recordRepository.GetByIdAsync(request.RecordId);

            if (recordToDelete == null)
            {
                throw new NoEntityFoundException($"There is no diagnosis medical record with id = {request.RecordId} to delete");
            }

            await _recordRepository.DeleteAsync(recordToDelete);
            return await Task.FromResult(_mapper.Map<DiagnosisMedicalRecordDto>(recordToDelete));
        }
    }
}
