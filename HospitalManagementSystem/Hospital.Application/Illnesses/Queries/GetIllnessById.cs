using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Illnesses.Responses;
using MediatR;

namespace Hospital.Application.Illnesses.Queries
{
    public record GetIllnessById(int IllnessId) : IRequest<IllnessDto>;

    public class GetIllnessByIdHandler : IRequestHandler<GetIllnessById, IllnessDto>
    {
        private readonly IIllnessRepository _illnessRepository;

        public GetIllnessByIdHandler(IIllnessRepository illnessRepository)
        {
            _illnessRepository = illnessRepository;
        }

        public Task<IllnessDto> Handle(GetIllnessById request, CancellationToken cancellationToken)
        {
            var illness = _illnessRepository.GetById(request.IllnessId);

            if (illness is null)
            {
                throw new NoEntityFoundException($"There is no illness with id {request.IllnessId}");
            }

            return Task.FromResult(IllnessDto.FromIllness(illness));
        }
    }
}
