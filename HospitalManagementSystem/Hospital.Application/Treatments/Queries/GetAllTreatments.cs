using Hospital.Application.Abstractions;
using Hospital.Application.Treatments.Responses;
using MediatR;

namespace Hospital.Application.Treatments.Queries
{
    public record GetAllTreatments() : IRequest<List<TreatmentDto>>;

    public class GetAllTreatmentsHandler : IRequestHandler<GetAllTreatments, List<TreatmentDto>>
    {
        private readonly ITreatmentRepository _treatmentRepository;

        public GetAllTreatmentsHandler(ITreatmentRepository treatmentRepository)
        {
            _treatmentRepository = treatmentRepository;
        }

        public Task<List<TreatmentDto>> Handle(GetAllTreatments request, CancellationToken cancellationToken)
        {
            var treatments = _treatmentRepository.GetAll();
            return Task.FromResult(treatments.Select(TreatmentDto.FromTreatment).ToList());
        }
    }
}
