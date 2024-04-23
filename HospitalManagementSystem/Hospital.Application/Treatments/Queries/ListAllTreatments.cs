using Hospital.Application.Abstractions;
using Hospital.Application.Treatments.Responses;
using MediatR;

namespace Hospital.Application.Treatments.Queries
{
    public record ListAllTreatments() : IRequest<List<TreatmentDto>>;

    public class ListAllTreatmentsHandler : IRequestHandler<ListAllTreatments, List<TreatmentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListAllTreatmentsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TreatmentDto>> Handle(ListAllTreatments request, CancellationToken cancellationToken)
        {
            var treatments = await _unitOfWork.TreatmentRepository.GetAllAsync();
            return await Task.FromResult(treatments.Select(TreatmentDto.FromTreatment).ToList());
        }
    }
}
