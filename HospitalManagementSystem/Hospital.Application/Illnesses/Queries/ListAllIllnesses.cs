using Hospital.Application.Abstractions;
using Hospital.Application.Illnesses.Responses;
using MediatR;

namespace Hospital.Application.Illnesses.Queries
{
    public record ListAllIllnesses() : IRequest<List<IllnessDto>>;

    public class ListAllIllnessesHandler : IRequestHandler<ListAllIllnesses, List<IllnessDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListAllIllnessesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<IllnessDto>> Handle(ListAllIllnesses request, CancellationToken cancellationToken)
        {
            var illnesses = await _unitOfWork.IllnessRepository.GetAllAsync();
            return await Task.FromResult(illnesses.Select(IllnessDto.FromIllness).ToList());
        }
    }
}
