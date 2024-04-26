using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Doctors.Queries
{
    public record ListAllDoctors() : IRequest<List<DoctorDto>>;

    public class ListAllDoctorsHandler : IRequestHandler<ListAllDoctors, List<DoctorDto>>
    {
        private readonly IRepository<Doctor> _doctorRepository;

        public ListAllDoctorsHandler(IRepository<Doctor> doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<List<DoctorDto>> Handle(ListAllDoctors request, CancellationToken cancellationToken)
        {
            var doctors = await _doctorRepository.GetAllAsync();
            return await Task.FromResult(doctors.Select(DoctorDto.FromDoctor).ToList());
        }
    }
}
