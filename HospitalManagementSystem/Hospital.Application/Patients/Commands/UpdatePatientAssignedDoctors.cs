using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Patients.Commands
{
    public record UpdatePatientAssignedDoctors(int Id, List<int> DoctorIds)
        : IRequest<PatientDto>;

    public class UpdatePatientAssignedDoctorsHandler
        : IRequestHandler<UpdatePatientAssignedDoctors, PatientDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePatientAssignedDoctorsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PatientDto> Handle(UpdatePatientAssignedDoctors request, CancellationToken cancellationToken)
        {
            var existingPatient = await _unitOfWork.PatientRepository.GetByIdAsync(request.Id);

            if (existingPatient != null)
            {
                existingPatient.DoctorsPatients = request.DoctorIds.Select(doctorId => new DoctorsPatients()
                {
                    DoctorId = doctorId,
                    PatientId = existingPatient.Id,
                }).ToList();

                await _unitOfWork.PatientRepository.UpdateAsync(existingPatient);

                return await Task.FromResult(PatientDto.FromPatient(existingPatient));
            }
            else
            {
                throw new NoEntityFoundException($"Cannot update non-existing patient with id {request.Id}");
            }
        }
    }
}
