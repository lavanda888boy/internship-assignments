using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Doctors.Queries
{
    public record GetDoctorsByProperty(Func<Doctor, bool> DoctorProperty) : IRequest<List<DoctorDto>>;

    public class GetDoctorsByPropertyHandler : IRequestHandler<GetDoctorsByProperty, List<DoctorDto>>
    {
        private readonly IRepository<Doctor> _doctorRepository;

        public GetDoctorsByPropertyHandler(IRepository<Doctor> doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public Task<List<DoctorDto>> Handle(GetDoctorsByProperty request, CancellationToken cancellationToken)
        {
            var doctors = _doctorRepository.SearchByProperty(request.DoctorProperty);

            if (doctors is null)
            {
                throw new NoEntityFoundException("No doctors with such properties exist");
            }

            return Task.FromResult(doctors.Select(DoctorDto.FromDoctor).ToList());
        }
    }
}
