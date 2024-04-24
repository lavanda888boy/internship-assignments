using Hospital.Application.Abstractions;
using Hospital.Application.MedicalRecords.Responses;
using MediatR;

namespace Hospital.Application.MedicalRecords.Queries
{
    public record ListAllRegularMedicalRecords()
        : IRequest<List<RegularMedicalRecordDto>>;

    public class ListAllRegularMedicalRecordsHandler
        : IRequestHandler<ListAllRegularMedicalRecords, List<RegularMedicalRecordDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListAllRegularMedicalRecordsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RegularMedicalRecordDto>> Handle(ListAllRegularMedicalRecords request, CancellationToken cancellationToken)
        {
            var medicalRecords = await _unitOfWork.RegularRecordRepository.GetAllAsync();
            return await Task.FromResult(medicalRecords.Select(RegularMedicalRecordDto.FromMedicalRecord).ToList());
        }
    }
}
