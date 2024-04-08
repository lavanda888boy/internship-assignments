using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using MediatR;

namespace Hospital.Application.Doctors.Queries
{
    public record ListAllDoctors() : IRequest<List<DoctorDto>>;

    public class ListAllDoctorsHandler : IRequestHandler<ListAllDoctors, List<DoctorDto>>
    {
        private readonly IDoctorRepository _doctorRepository;

        public ListAllDoctorsHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public Task<List<DoctorDto>> Handle(ListAllDoctors request, CancellationToken cancellationToken)
        {
            var doctors = _doctorRepository.GetAll();
            return Task.FromResult(doctors.Select(DoctorDto.FromDoctor).ToList());
        }
    }
}
