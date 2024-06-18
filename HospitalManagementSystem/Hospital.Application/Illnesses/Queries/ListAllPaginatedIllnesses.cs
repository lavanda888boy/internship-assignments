using Hospital.Application.Abstractions;
using Hospital.Application.Common;
using Hospital.Application.Illnesses.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Illnesses.Queries
{
    public record ListAllPaginatedIllnesses(int PageNumber = 1, int PageSize = 20) : IRequest<PaginatedResult<IllnessDto>>;

    public class ListAllPaginatedIllnessesHandler : IRequestHandler<ListAllPaginatedIllnesses, PaginatedResult<IllnessDto>>
    {
        private readonly IRepository<Illness> _illnessRepository;

        public ListAllPaginatedIllnessesHandler(IRepository<Illness> illnessRepository)
        {
            _illnessRepository = illnessRepository;
        }

        public async Task<PaginatedResult<IllnessDto>> Handle(ListAllPaginatedIllnesses request, CancellationToken cancellationToken)
        {
            var paginatedIllnesses = await _illnessRepository.GetAllPaginatedAsync(request.PageNumber, request.PageSize);
            return await Task.FromResult(new PaginatedResult<IllnessDto>
            {
                TotalItems = paginatedIllnesses.TotalItems,
                Items = paginatedIllnesses.Items.Select(IllnessDto.FromIllness).ToList()
            });
        }
    }
}
