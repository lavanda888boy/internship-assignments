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
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDoctorAssignedPatientsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DoctorDto> Handle(UpdateDoctorAssignedPatients request, CancellationToken cancellationToken)
        {
            var existingDoctor = await _unitOfWork.DoctorRepository.GetByIdAsync(request.Id);
            if (existingDoctor != null)
            {
                existingDoctor.DoctorsPatients.Clear();

                existingDoctor.DoctorsPatients = request.PatientIds.Select(patientId => new DoctorsPatients()
                {
                    DoctorId = existingDoctor.Id,
                    PatientId = patientId
                }).ToList();

                await _unitOfWork.DoctorRepository.UpdateAsync(existingDoctor);

                return await Task.FromResult(DoctorDto.FromDoctor(existingDoctor));
            }
            else
            {
                throw new NoEntityFoundException($"Cannot update non-existing doctor with id {request.Id}");
            }
        }
    }
}
