using Hospital.Application.Abstractions;
using Hospital.Application.Illnesses.Responses;
using MediatR;

namespace Hospital.Application.Illnesses.Queries
{
    public record GetAllIllnesses() : IRequest<List<IllnessDto>>;

    public class GetAllIllnessesHandler : IRequestHandler<GetAllIllnesses, List<IllnessDto>>
    {
        private readonly IIllnessRepository _illnessRepository;

        public GetAllIllnessesHandler(IIllnessRepository illnessRepository)
        {
            _illnessRepository = illnessRepository;
        }

        public Task<List<IllnessDto>> Handle(GetAllIllnesses request, CancellationToken cancellationToken)
        {
            var illnesses = _illnessRepository.GetAll();
            return Task.FromResult(illnesses.Select(IllnessDto.FromIllness).ToList());
        }
    }
}
