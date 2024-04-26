using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Treatments.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Treatments.Queries
{
    public record SearchTreatmentByPrescribedMedicine(string TreatmentMedicine) : IRequest<List<TreatmentDto>>;

    public class SearchTreatmentByPrescribedMedicineHandler 
        : IRequestHandler<SearchTreatmentByPrescribedMedicine, List<TreatmentDto>>
    {
        private readonly IRepository<Treatment> _treatmentRepository;

        public SearchTreatmentByPrescribedMedicineHandler(IRepository<Treatment> treatmentRepository)
        {
            _treatmentRepository = treatmentRepository;
        }

        public async Task<List<TreatmentDto>> Handle(SearchTreatmentByPrescribedMedicine request, CancellationToken cancellationToken)
        {
            var treatments = await _treatmentRepository.SearchByPropertyAsync(t => 
                    t.PrescribedMedicine == request.TreatmentMedicine);

            if (treatments.Count == 0)
            {
                throw new NoEntityFoundException("No treatments with such prescribed medicines exist");
            }

            return await Task.FromResult(treatments.Select(TreatmentDto.FromTreatment).ToList());
        }
    }
}
