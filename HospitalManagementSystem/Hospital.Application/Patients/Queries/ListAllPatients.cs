using Hospital.Application.Abstractions;
using Hospital.Application.Patients.Responses;
using MediatR;

namespace Hospital.Application.Patients.Queries
{
    public record ListAllPatients() : IRequest<List<PatientDto>>;

    public class ListAllPatientsHandler : IRequestHandler<ListAllPatients, List<PatientDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListAllPatientsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<PatientDto>> Handle(ListAllPatients request, CancellationToken cancellationToken)
        {
            var patients = await _unitOfWork.PatientRepository.GetAllAsync();
            return await Task.FromResult(patients.Select(PatientDto.FromPatient).ToList());
        }
    }
}
