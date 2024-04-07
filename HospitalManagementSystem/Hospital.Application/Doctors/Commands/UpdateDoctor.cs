using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Doctors.Commands
{
    public record UpdateDoctor(int Id, string Name, string Surname, string Address,
        string PhoneNumber, int DepartmentId, List<int> AssignedPatientIds, 
        int WorkingHoursId, TimeSpan StartShift, TimeSpan EndShift) : IRequest<DoctorDto>;

    public class UpdateDoctorHandler : IRequestHandler<UpdateDoctor, DoctorDto>
    {
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Patient> _patientRepository;

        public UpdateDoctorHandler(IRepository<Doctor> doctorRepository, 
            IRepository<Department> departmentRepository,
            IRepository<Patient> patientRepository)
        {
            _doctorRepository = doctorRepository;
            _departmentRepository = departmentRepository;
            _patientRepository = patientRepository;
        }

        public Task<DoctorDto> Handle(UpdateDoctor request, CancellationToken cancellationToken)
        {
            var updatedDoctor = new Doctor()
            {
                Id = request.Id,
                Name = request.Name,
                Surname = request.Surname,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Department = _departmentRepository.GetById(request.DepartmentId),
                WorkingHours = new DoctorWorkingHours()
                {
                    Id = request.WorkingHoursId,
                    StartShift = request.StartShift,
                    EndShift = request.EndShift
                }
            };

            var existingDoctor = _doctorRepository.GetById(request.Id);
            if (existingDoctor != null)
            {
                updatedDoctor.AssignedPatients = existingDoctor.AssignedPatients;

                var doctorPatientsIds = existingDoctor.AssignedPatients
                                                      .Select(p => p.Id);
                var patientsIdsToAdd = request.AssignedPatientIds.Except(doctorPatientsIds);
                var patientsIdsToRemove = doctorPatientsIds.Except(request.AssignedPatientIds);

                foreach (var item in patientsIdsToAdd)
                {
                    var patient = _patientRepository.GetById(item);
                    if (updatedDoctor.AddPatient(patient))
                    {
                        patient.AddDoctor(updatedDoctor);
                        _patientRepository.Update(patient);
                    }
                    else
                    {
                        throw new DoctorAssignedPatientsLimitExceeded("Too many patients to add to the existing doctor");
                    }
                }

                foreach (var item in patientsIdsToRemove)
                {
                    var patient = _patientRepository.GetById(item);
                    updatedDoctor.RemovePatient(item);
                    patient.RemoveDoctor(updatedDoctor.Id);
                    _patientRepository.Update(patient);
                }

                _doctorRepository.Update(updatedDoctor);
                return Task.FromResult(DoctorDto.FromDoctor(updatedDoctor));
            }
            else
            {
                throw new NoEntityFoundException($"Cannot update non-existing doctor with id {request.Id}");
            }
        }
    }
}
