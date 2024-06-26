﻿using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Patients.Queries
{
    public record GetPatientById(int PatientId) : IRequest<PatientFullInfoDto>;

    public class GetPatientByIdHandler : IRequestHandler<GetPatientById, PatientFullInfoDto>
    {
        private readonly IRepository<Patient> _patientRepository;
        private readonly IMapper _mapper;

        public GetPatientByIdHandler(IRepository<Patient> patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<PatientFullInfoDto> Handle(GetPatientById request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);

            if (patient == null)
            {
                throw new NoEntityFoundException($"Patient with id {request.PatientId} does not exist");
            }

            return await Task.FromResult(_mapper.Map<PatientFullInfoDto>(patient));
        }
    }
}
