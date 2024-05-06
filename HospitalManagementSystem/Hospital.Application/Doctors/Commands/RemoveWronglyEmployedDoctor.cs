using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Doctors.Commands
{
    public record RemoveWronglyEmployedDoctor(int DoctorId) : IRequest<DoctorDto>;

    public class RemoveWronglyEmployedDoctorHandler : IRequestHandler<RemoveWronglyEmployedDoctor, DoctorDto>
    {
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IMapper _mapper;

        public RemoveWronglyEmployedDoctorHandler(IRepository<Doctor> doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<DoctorDto> Handle(RemoveWronglyEmployedDoctor request, CancellationToken cancellationToken)
        {
            var doctorToDelete = await _doctorRepository.GetByIdAsync(request.DoctorId);

            if (doctorToDelete == null)
            {
                throw new NoEntityFoundException($"There is no doctor with id = {request.DoctorId} to delete");
            }

            await _doctorRepository.DeleteAsync(doctorToDelete);
            return await Task.FromResult(_mapper.Map<DoctorDto>(doctorToDelete));
        }
    }
}
