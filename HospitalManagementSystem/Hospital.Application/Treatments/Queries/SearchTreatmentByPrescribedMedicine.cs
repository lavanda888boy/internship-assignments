using Hospital.Application.Abstractions;
using Hospital.Application.Common;
using Hospital.Application.Exceptions;
using Hospital.Application.Treatments.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Treatments.Queries
{
    public record SearchTreatmentByPrescribedMedicine(string TreatmentMedicine, int PageNumber, int PageSize) 
        : IRequest<PaginatedResult<TreatmentDto>>;

    public class SearchTreatmentByPrescribedMedicineHandler 
        : IRequestHandler<SearchTreatmentByPrescribedMedicine, PaginatedResult<TreatmentDto>>
    {
        private readonly IRepository<Treatment> _treatmentRepository;

        public SearchTreatmentByPrescribedMedicineHandler(IRepository<Treatment> treatmentRepository)
        {
            _treatmentRepository = treatmentRepository;
        }

        public async Task<PaginatedResult<TreatmentDto>> Handle(SearchTreatmentByPrescribedMedicine request,
            CancellationToken cancellationToken)
        {
            var paginatedTreatments = await _treatmentRepository.SearchByPropertyPaginatedAsync(t => 
                    t.PrescribedMedicine == request.TreatmentMedicine, request.PageNumber, request.PageSize);

            if (paginatedTreatments.Items.Count == 0)
            {
                throw new NoEntityFoundException("No treatments with such prescribed medicines exist");
            }

            return await Task.FromResult(new PaginatedResult<TreatmentDto>
            {
                TotalItems = paginatedTreatments.TotalItems,
                Items = paginatedTreatments.Items.Select(TreatmentDto.FromTreatment).ToList()
            });
        }
    }
}
