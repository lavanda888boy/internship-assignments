using Hospital.Domain.Models.Utility;
using Hospital.Domain.Models;
using MediatR;
using Hospital.Application.Illnesses.Responses;
using Hospital.Application.Abstractions;

namespace Hospital.Application.Illnesses.Commands
{
    public record RegisterExistingIllness(int Id, string Name, IllnessSeverity Severity) 
        : IRequest<IllnessDto>;

    public class RegisterExistingIllnessHandler : IRequestHandler<RegisterExistingIllness, IllnessDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterExistingIllnessHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IllnessDto> Handle(RegisterExistingIllness request, CancellationToken cancellationToken)
        {
            try
            {
                var illness = new Illness
                {
                    Id = request.Id,
                    Name = request.Name,
                    Severity = request.Severity
                };

                await _unitOfWork.BeginTransactionAsync();
                var newIllness = _unitOfWork.IllnessRepository.Add(illness);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return await Task.FromResult(IllnessDto.FromIllness(newIllness));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
