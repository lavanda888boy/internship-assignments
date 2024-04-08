using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Illnesses.Responses;
using Hospital.Domain.Models.Utility;
using MediatR;

namespace Hospital.Application.Illnesses.Queries
{
    public record SearchIllnessBySeverity(IllnessSeverity IllnessSeverity)
        : IRequest<List<IllnessDto>>;

    public class SearchIllnessBySeverityHandler : IRequestHandler<SearchIllnessBySeverity,
        List<IllnessDto>>
    {
        private readonly IIllnessRepository _illnessRepository;

        public SearchIllnessBySeverityHandler(IIllnessRepository illnessRepository)
        {
            _illnessRepository = illnessRepository;
        }

        public Task<List<IllnessDto>> Handle(SearchIllnessBySeverity request, CancellationToken cancellationToken)
        {
            var illnesses = _illnessRepository.SearchByProperty(i => i.IllnessSeverity == request.IllnessSeverity);

            if (illnesses is null)
            {
                throw new NoEntityFoundException("No illnesses with such severity exist");
            }

            return Task.FromResult(illnesses.Select(IllnessDto.FromIllness).ToList());
        }
    }
}
