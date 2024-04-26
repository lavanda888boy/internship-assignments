using Hospital.Application.Abstractions;
using Hospital.Application.Illnesses.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Illnesses.Queries
{
    public record ListAllIllnesses() : IRequest<List<IllnessDto>>;

    public class ListAllIllnessesHandler : IRequestHandler<ListAllIllnesses, List<IllnessDto>>
    {
        private readonly IRepository<Illness> _illnessRepository;

        public ListAllIllnessesHandler(IRepository<Illness> illnessRepository)
        {
            _illnessRepository = illnessRepository;
        }

        public async Task<List<IllnessDto>> Handle(ListAllIllnesses request, CancellationToken cancellationToken)
        {
            var illnesses = await _illnessRepository.GetAllAsync();
            return await Task.FromResult(illnesses.Select(IllnessDto.FromIllness).ToList());
        }
    }
}
