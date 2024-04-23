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
        private readonly IUnitOfWork _unitOfWork;

        public SearchTreatmentByPrescribedMedicineHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TreatmentDto>> Handle(SearchTreatmentByPrescribedMedicine request, CancellationToken cancellationToken)
        {
            var treatments = await _unitOfWork.TreatmentRepository.SearchByPropertyAsync(t => 
                    t.PrescribedMedicine == request.TreatmentMedicine);

            if (treatments.Count == 0)
            {
                throw new NoEntityFoundException("No treatments with such prescribed medicines exist");
            }

            return await Task.FromResult(treatments.Select(TreatmentDto.FromTreatment).ToList());
        }
    }
}
