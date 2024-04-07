﻿using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Doctors.Commands
{
    public record DeleteDoctor(int DoctorId) : IRequest<int>;

    public class DeleteDoctorHandler : IRequestHandler<DeleteDoctor, int>
    {
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<Patient> _patientRepository;

        public DeleteDoctorHandler(IRepository<Doctor> doctorRepository, 
            IRepository<Patient> patientRepository)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public Task<int> Handle(DeleteDoctor request, CancellationToken cancellationToken)
        {
            var doctorToDelete = _doctorRepository.GetById(request.DoctorId);
            if (doctorToDelete != null)
            {
                _doctorRepository.Delete(request.DoctorId);
                foreach (var item in doctorToDelete.AssignedPatients)
                {
                    item.RemoveDoctor(request.DoctorId);
                    _patientRepository.Update(item);
                }

                return Task.FromResult(request.DoctorId);
            }
            else
            {
                throw new NoEntityFoundException($"Cannot delete non-existing doctor with id {request.Id}");
            }
        }
    }
}
