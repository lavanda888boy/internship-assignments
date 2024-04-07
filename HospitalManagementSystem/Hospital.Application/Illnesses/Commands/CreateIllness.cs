using Hospital.Domain.Models.Utility;
using Hospital.Domain.Models;
using MediatR;
using Hospital.Application.Illnesses.Responses;
using Hospital.Application.Abstractions;

namespace Hospital.Application.Illnesses.Commands
{
    public record CreateIllness(int Id, string Name, IllnessSeverity IllnessSeverity) 
        : IRequest<IllnessDto>;

    public class CreateIllnessHandler : IRequestHandler<CreateIllness, IllnessDto>
    {
        private readonly IIllnessRepository _illnessRepository;

        public CreateIllnessHandler(IIllnessRepository illnessRepository)
        {
            _illnessRepository = illnessRepository;
        }

        public Task<IllnessDto> Handle(CreateIllness request, CancellationToken cancellationToken)
        {
            var newIllness = new Illness
            {
                Id = request.Id,
                Name = request.Name,
                IllnessSeverity = request.IllnessSeverity
            };

            var createdIllness = _illnessRepository.Create(newIllness);
            return Task.FromResult(IllnessDto.FromIllness(createdIllness));
        }
    }
}
