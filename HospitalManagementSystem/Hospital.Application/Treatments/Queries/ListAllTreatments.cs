using Hospital.Application.Abstractions;
using Hospital.Application.Treatments.Responses;
using MediatR;

namespace Hospital.Application.Treatments.Queries
{
    public record ListAllTreatments() : IRequest<List<TreatmentDto>>;

    public class ListAllTreatmentsHandler : IRequestHandler<ListAllTreatments, List<TreatmentDto>>
    {
        private readonly ITreatmentRepository _treatmentRepository;

        public ListAllTreatmentsHandler(ITreatmentRepository treatmentRepository)
        {
            _treatmentRepository = treatmentRepository;
        }

        public Task<List<TreatmentDto>> Handle(ListAllTreatments request, CancellationToken cancellationToken)
        {
            var treatments = _treatmentRepository.GetAll();
            return Task.FromResult(treatments.Select(TreatmentDto.FromTreatment).ToList());
        }
    }
}
