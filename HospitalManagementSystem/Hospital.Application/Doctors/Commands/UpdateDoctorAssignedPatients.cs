using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Doctors.Commands
{
    public record UpdateDoctorAssignedPatients(int Id, List<int> PatientIds) : IRequest<DoctorDto>;

    public class UpdateDoctorAssignedPatientsHandler : IRequestHandler<UpdateDoctorAssignedPatients, DoctorDto>
    {
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IMapper _mapper;

        public UpdateDoctorAssignedPatientsHandler(IRepository<Doctor> doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<DoctorDto> Handle(UpdateDoctorAssignedPatients request, CancellationToken cancellationToken)
        {
            var existingDoctor = await _doctorRepository.GetByIdAsync(request.Id);
            if (existingDoctor != null)
            {
                existingDoctor.DoctorsPatients.Clear();

                existingDoctor.DoctorsPatients = request.PatientIds.Select(patientId => new DoctorsPatients()
                {
                    DoctorId = existingDoctor.Id,
                    PatientId = patientId
                }).ToList();

                await _doctorRepository.UpdateAsync(existingDoctor);

                return await Task.FromResult(_mapper.Map<DoctorDto>(existingDoctor));
            }
            else
            {
                throw new NoEntityFoundException($"Cannot update non-existing doctor with id {request.Id}");
            }
        }
    }
}
