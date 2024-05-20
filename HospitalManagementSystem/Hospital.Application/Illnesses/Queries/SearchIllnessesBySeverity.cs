using Hospital.Application.Abstractions;
using Hospital.Application.Common;
using Hospital.Application.Exceptions;
using Hospital.Application.Illnesses.Responses;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using MediatR;

namespace Hospital.Application.Illnesses.Queries
{
    public record SearchIllnessesBySeverity(IllnessSeverity IllnessSeverity, int PageNumber, int PageSize)
        : IRequest<PaginatedResult<IllnessDto>>;

    public class SearchIllnessesBySeverityHandler : IRequestHandler<SearchIllnessesBySeverity,
        PaginatedResult<IllnessDto>>
    {
        private readonly IRepository<Illness> _illnessRepository;

        public SearchIllnessesBySeverityHandler(IRepository<Illness> illnessRepository)
        {
            _illnessRepository = illnessRepository;
        }

        public async Task<PaginatedResult<IllnessDto>> Handle(SearchIllnessesBySeverity request, CancellationToken cancellationToken)
        {
            var paginatedIllnesses = await _illnessRepository.SearchByPropertyPaginatedAsync(i => 
                    i.Severity == request.IllnessSeverity, request.PageNumber, request.PageSize);

            if (paginatedIllnesses.Items.Count == 0)
            {
                throw new NoEntityFoundException("No illnesses with such severity exist");
            }

            return await Task.FromResult(new PaginatedResult<IllnessDto>
            {
                TotalItems = paginatedIllnesses.TotalItems,
                Items = paginatedIllnesses.Items.Select(IllnessDto.FromIllness).ToList()
            });
        }
    }
}
