using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Illnesses.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Illnesses.Queries
{
    public record GetAllIllnesses() : IRequest<List<IllnessDto>>;

    public class GetAllIllnessesHandler : IRequestHandler<GetAllIllnesses, List<IllnessDto>>
    {
        private readonly IRepository<Illness> _illnessRepository;

        public GetAllIllnessesHandler(IRepository<Illness> illnessRepository)
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
