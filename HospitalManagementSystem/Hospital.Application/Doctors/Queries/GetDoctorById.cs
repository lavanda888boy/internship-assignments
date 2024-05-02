using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Doctors.Queries
{
    public record GetDoctorById(int DoctorId) : IRequest<DoctorDto>;

    public class GetDoctorByIdHandler : IRequestHandler<GetDoctorById, DoctorDto>
    {
        private readonly IRepository<Doctor> _doctorRepository;

        public GetDoctorByIdHandler(IRepository<Doctor> doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<DoctorDto> Handle(GetDoctorById request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);

            if (doctor == null)
            {
                throw new NoEntityFoundException($"Doctor with id {request.DoctorId} does not exist");
            }

            return await Task.FromResult(DoctorDto.FromDoctor(doctor));
        }
    }
}
