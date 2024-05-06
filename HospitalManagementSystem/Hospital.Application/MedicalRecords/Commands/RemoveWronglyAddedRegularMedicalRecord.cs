using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.MedicalRecords.Commands
{
    public record RemoveWronglyAddedRegularMedicalRecord(int RecordId) : IRequest<RegularMedicalRecordDto>;

    public class RemoveWronglyAddedRegularMedicalRecordHandler :
        IRequestHandler<RemoveWronglyAddedRegularMedicalRecord, RegularMedicalRecordDto>
    {
        private readonly IRepository<RegularMedicalRecord> _recordRepository;
        private readonly IMapper _mapper;

        public RemoveWronglyAddedRegularMedicalRecordHandler(IRepository<RegularMedicalRecord> recordRepository,
            IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        public async Task<RegularMedicalRecordDto> Handle(RemoveWronglyAddedRegularMedicalRecord request,
            CancellationToken cancellationToken)
        {
            var recordToDelete = await _recordRepository.GetByIdAsync(request.RecordId);

            if (recordToDelete == null)
            {
                throw new NoEntityFoundException($"There is no regular medical record with id = {request.RecordId} to delete");
            }

            await _recordRepository.DeleteAsync(recordToDelete);
            return await Task.FromResult(_mapper.Map<RegularMedicalRecordDto>(recordToDelete));
        }
    }
}
