using AutoMapper;
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
        private readonly IMapper _mapper;

        public ListAllDoctorsHandler(IRepository<Doctor> doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<List<DoctorDto>> Handle(ListAllDoctors request, CancellationToken cancellationToken)
        {
            var doctors = await _doctorRepository.GetAllAsync();
            var doctorDtos = _mapper.Map<List<DoctorDto>>(doctors);
            return await Task.FromResult(doctorDtos);
        }
    }
}
