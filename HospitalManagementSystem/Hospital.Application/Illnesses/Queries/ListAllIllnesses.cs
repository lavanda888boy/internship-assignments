using Hospital.Application.Abstractions;
using Hospital.Application.Illnesses.Responses;
using MediatR;

namespace Hospital.Application.Illnesses.Queries
{
    public record ListAllIllnesses() : IRequest<List<IllnessDto>>;

    public class ListAllIllnessesHandler : IRequestHandler<ListAllIllnesses, List<IllnessDto>>
    {
        private readonly IIllnessRepository _illnessRepository;

        public ListAllIllnessesHandler(IIllnessRepository illnessRepository)
        {
            _illnessRepository = illnessRepository;
        }

        public Task<List<IllnessDto>> Handle(ListAllIllnesses request, CancellationToken cancellationToken)
        {
            var illnesses = _illnessRepository.GetAll();
            return Task.FromResult(illnesses.Select(IllnessDto.FromIllness).ToList());
        }
    }
}
