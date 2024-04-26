using Hospital.Domain.Models.Utility;
using Hospital.Domain.Models;
using MediatR;
using Hospital.Application.Illnesses.Responses;
using Hospital.Application.Abstractions;

namespace Hospital.Application.Illnesses.Commands
{
    public record RegisterExistingIllness(string Name, IllnessSeverity Severity) 
        : IRequest<IllnessDto>;

    public class RegisterExistingIllnessHandler : IRequestHandler<RegisterExistingIllness, IllnessDto>
    {
        private readonly IRepository<Illness> _illnessRepository;

        public RegisterExistingIllnessHandler(IRepository<Illness> illnessRepository)
        {
            _illnessRepository = illnessRepository;
        }

        public async Task<IllnessDto> Handle(RegisterExistingIllness request, CancellationToken cancellationToken)
        {
            var illness = new Illness
            {
                Name = request.Name,
                Severity = request.Severity
            };

            var newIllness = await _illnessRepository.AddAsync(illness);

            return await Task.FromResult(IllnessDto.FromIllness(newIllness));
        }
    }
}
