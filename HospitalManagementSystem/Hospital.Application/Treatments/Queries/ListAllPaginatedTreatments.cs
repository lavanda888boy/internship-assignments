using Hospital.Application.Abstractions;
using Hospital.Application.Common;
using Hospital.Application.Treatments.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Treatments.Queries
{
    public record ListAllPaginatedTreatments(int PageNumber, int PageSize) : IRequest<PaginatedResult<TreatmentDto>>;

    public class ListAllPaginatedTreatmentsHandler : IRequestHandler<ListAllPaginatedTreatments, PaginatedResult<TreatmentDto>>
    {
        private readonly IRepository<Treatment> _treatmentRepository;

        public ListAllPaginatedTreatmentsHandler(IRepository<Treatment> treatmentRepository)
        {
            _treatmentRepository = treatmentRepository;
        }

        public async Task<PaginatedResult<TreatmentDto>> Handle(ListAllPaginatedTreatments request, CancellationToken cancellationToken)
        {
            var paginatedTreatments = await _treatmentRepository.GetAllPaginatedAsync(request.PageNumber, request.PageSize);
            return await Task.FromResult(new PaginatedResult<TreatmentDto>
            {
                TotalItems = paginatedTreatments.TotalItems,
                Items = paginatedTreatments.Items.Select(TreatmentDto.FromTreatment).ToList()
            });
        }
    }
}
