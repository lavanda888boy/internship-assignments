﻿using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Doctors.Commands
{
    public record UpdateDoctorAssignedPatients(int Id, List<int> PatientIds) : IRequest<int>;

    public class UpdateDoctorAssignedPatientsHandler : IRequestHandler<UpdateDoctorAssignedPatients, int>
    {
        private readonly IRepository<Doctor> _doctorRepository;

        public UpdateDoctorAssignedPatientsHandler(IRepository<Doctor> doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<int> Handle(UpdateDoctorAssignedPatients request, CancellationToken cancellationToken)
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

                return await Task.FromResult(existingDoctor.Id);
            }
            else
            {
                throw new NoEntityFoundException($"Cannot update non-existing doctor with id {request.Id}");
            }
        }
    }
}
