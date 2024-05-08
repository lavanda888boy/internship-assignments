using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Doctors.Queries
{
    public record GetDoctorById(int DoctorId) : IRequest<DoctorFullInfoDto>;

    public class GetDoctorByIdHandler : IRequestHandler<GetDoctorById, DoctorFullInfoDto>
    {
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IMapper _mapper;

        public GetDoctorByIdHandler(IRepository<Doctor> doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<DoctorFullInfoDto> Handle(GetDoctorById request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);

            if (doctor == null)
            {
                throw new NoEntityFoundException($"Doctor with id {request.DoctorId} does not exist");
            }

            return await Task.FromResult(_mapper.Map<DoctorFullInfoDto>(doctor));
        }
    }
}
