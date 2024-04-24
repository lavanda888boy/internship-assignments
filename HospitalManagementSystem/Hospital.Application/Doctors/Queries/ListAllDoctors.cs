using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using MediatR;

namespace Hospital.Application.Doctors.Queries
{
    public record ListAllDoctors() : IRequest<List<DoctorDto>>;

    public class ListAllDoctorsHandler : IRequestHandler<ListAllDoctors, List<DoctorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListAllDoctorsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<DoctorDto>> Handle(ListAllDoctors request, CancellationToken cancellationToken)
        {
            var doctors = await _unitOfWork.DoctorRepository.GetAllAsync();
            return await Task.FromResult(doctors.Select(DoctorDto.FromDoctor).ToList());
        }
    }
}
