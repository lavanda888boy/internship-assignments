using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Doctors.Responses;
using MediatR;

namespace Hospital.Application.Doctors.Queries
{
    public record GetDoctorById(int DoctorId) : IRequest<DoctorDto>;

    public class GetDoctorByIdHandler : IRequestHandler<GetDoctorById, DoctorDto>
    {
        private readonly IDoctorRepository _doctorRepository;

        public GetDoctorByIdHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public Task<DoctorDto> Handle(GetDoctorById request, CancellationToken cancellationToken)
        {
            var doctor = _doctorRepository.GetById(request.DoctorId);

            if (doctor is null)
            {
                throw new NoEntityFoundException($"There is no doctor with id {request.DoctorId}");
            }

            return Task.FromResult(DoctorDto.FromDoctor(doctor));
        }
    }
}
