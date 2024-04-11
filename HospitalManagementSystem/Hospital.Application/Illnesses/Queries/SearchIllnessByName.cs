using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Illnesses.Responses;
using MediatR;

namespace Hospital.Application.Illnesses.Queries
{
    public record SearchIllnessByName(string IllnessName) : IRequest<IllnessDto>;

    public class SearchIllnessByNameHandler : IRequestHandler<SearchIllnessByName, IllnessDto>
    {
        private readonly IIllnessRepository _illnessRepository;

        public SearchIllnessByNameHandler(IIllnessRepository illnessRepository)
        {
            _illnessRepository = illnessRepository;
        }

        public Task<IllnessDto> Handle(SearchIllnessByName request, CancellationToken cancellationToken)
        {
            var illnesses = _illnessRepository.SearchByProperty(i => i.Name == request.IllnessName);

            if (illnesses.Count == 0)
            {
                throw new NoEntityFoundException("No illness with such name exists");
            }

            return Task.FromResult(IllnessDto.FromIllness(illnesses[0]));
        }
    }
}

