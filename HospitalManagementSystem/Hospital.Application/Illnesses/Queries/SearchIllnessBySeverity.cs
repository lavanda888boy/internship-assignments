using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Illnesses.Responses;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using MediatR;

namespace Hospital.Application.Illnesses.Queries
{
    public record SearchIllnessBySeverity(IllnessSeverity IllnessSeverity)
        : IRequest<List<IllnessDto>>;

    public class SearchIllnessBySeverityHandler : IRequestHandler<SearchIllnessBySeverity,
        List<IllnessDto>>
    {
        private readonly IRepository<Illness> _illnessRepository;

        public SearchIllnessBySeverityHandler(IRepository<Illness> illnessRepository)
        {
            _illnessRepository = illnessRepository;
        }

        public async Task<List<IllnessDto>> Handle(SearchIllnessBySeverity request, CancellationToken cancellationToken)
        {
            var illnesses = await _illnessRepository.SearchByPropertyAsync(i => 
                    i.Severity == request.IllnessSeverity);

            if (illnesses.Count == 0)
            {
                throw new NoEntityFoundException("No illnesses with such severity exist");
            }

            return await Task.FromResult(illnesses.Select(IllnessDto.FromIllness).ToList());
        }
    }
}
