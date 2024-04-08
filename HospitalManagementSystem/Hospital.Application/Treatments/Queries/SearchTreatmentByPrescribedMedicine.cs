using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Treatments.Responses;
using MediatR;

namespace Hospital.Application.Treatments.Queries
{
    public record SearchTreatmentByPrescribedMedicine(string TreatmentMedicine) : IRequest<List<TreatmentDto>>;

    public class SearchTreatmentByPrescribedMedicineHandler 
        : IRequestHandler<SearchTreatmentByPrescribedMedicine, List<TreatmentDto>>
    {
        private readonly ITreatmentRepository _treatmentRepository;

        public SearchTreatmentByPrescribedMedicineHandler(ITreatmentRepository treatmentRepository)
        {
            _treatmentRepository = treatmentRepository;
        }

        public Task<List<TreatmentDto>> Handle(SearchTreatmentByPrescribedMedicine request, CancellationToken cancellationToken)
        {
            var treatments = _treatmentRepository.SearchByProperty(t => t.PrescribedMedicine == request.TreatmentMedicine);

            if (treatments is null)
            {
                throw new NoEntityFoundException("No treatments with such prescribed medicines exist");
            }

            return Task.FromResult(treatments.Select(TreatmentDto.FromTreatment).ToList());
        }
    }
}
