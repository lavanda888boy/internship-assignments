using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using MediatR;

namespace Hospital.Application.Doctors.Queries
{
    public record GetAllDoctors() : IRequest<List<DoctorDto>>;

    public class GetAllDoctorsHandler : IRequestHandler<GetAllDoctors, List<DoctorDto>>
    {
        private readonly IDoctorRepository _doctorRepository;

        public GetAllDoctorsHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public Task<List<DoctorDto>> Handle(GetAllDoctors request, CancellationToken cancellationToken)
        {
            var doctors = _doctorRepository.GetAll();
            return Task.FromResult(doctors.Select(DoctorDto.FromDoctor).ToList());
        }
    }
}
