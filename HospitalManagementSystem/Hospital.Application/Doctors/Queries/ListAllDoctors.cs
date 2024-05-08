using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Doctors.Queries
{
    public record ListAllDoctors() : IRequest<List<DoctorFullInfoDto>>;

    public class ListAllDoctorsHandler : IRequestHandler<ListAllDoctors, List<DoctorFullInfoDto>>
    {
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IMapper _mapper;

        public ListAllDoctorsHandler(IRepository<Doctor> doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<List<DoctorFullInfoDto>> Handle(ListAllDoctors request, CancellationToken cancellationToken)
        {
            var doctors = await _doctorRepository.GetAllAsync();
            var doctorDtos = _mapper.Map<List<DoctorFullInfoDto>>(doctors);
            return await Task.FromResult(doctorDtos);
        }
    }
}
