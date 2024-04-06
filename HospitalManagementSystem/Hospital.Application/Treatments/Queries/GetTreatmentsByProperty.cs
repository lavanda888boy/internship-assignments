using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Treatments.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Treatments.Queries
{
    public record GetTreatmentsByProperty(Func<Treatment, bool> TreatmentProperty) : IRequest<List<TreatmentDto>>;

    public class GetTreatmentsByPropertyHandler : IRequestHandler<GetTreatmentsByProperty, List<TreatmentDto>>
    {
        private readonly IRepository<Treatment> _treatmentRepository;

        public GetTreatmentsByPropertyHandler(IRepository<Treatment> treatmentRepository)
        {
            _treatmentRepository = treatmentRepository;
        }

        public Task<List<TreatmentDto>> Handle(GetTreatmentsByProperty request, CancellationToken cancellationToken)
        {
            var treatments = _treatmentRepository.SearchByProperty(request.TreatmentProperty);

            if (treatments is null)
            {
                throw new NoEntityFoundException("No treatments with such properties exist");
            }

            return Task.FromResult(treatments.Select(TreatmentDto.FromTreatment).ToList());
        }
    }
}
