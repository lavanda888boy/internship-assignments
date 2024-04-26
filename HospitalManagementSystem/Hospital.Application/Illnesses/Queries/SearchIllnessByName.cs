using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Illnesses.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Illnesses.Queries
{
    public record SearchIllnessByName(string IllnessName) : IRequest<IllnessDto>;

    public class SearchIllnessByNameHandler : IRequestHandler<SearchIllnessByName, IllnessDto>
    {
        private readonly IRepository<Illness> _illnessRepository;

        public SearchIllnessByNameHandler(IRepository<Illness> illnessRepository)
        {
            _illnessRepository = illnessRepository;
        }

        public async Task<IllnessDto> Handle(SearchIllnessByName request, CancellationToken cancellationToken)
        {
            var illnesses = await _illnessRepository.SearchByPropertyAsync(i => 
                    i.Name == request.IllnessName);

            if (illnesses.Count == 0)
            {
                throw new NoEntityFoundException("No illness with such name exists");
            }

            return await Task.FromResult(IllnessDto.FromIllness(illnesses[0]));
        }
    }
}

