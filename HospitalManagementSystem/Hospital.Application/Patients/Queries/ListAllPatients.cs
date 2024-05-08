using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Patients.Queries
{
    public record ListAllPatients() : IRequest<List<PatientFullInfoDto>>;

    public class ListAllPatientsHandler : IRequestHandler<ListAllPatients, List<PatientFullInfoDto>>
    {
        private readonly IRepository<Patient> _patientRepository;
        private readonly IMapper _mapper;

        public ListAllPatientsHandler(IRepository<Patient> patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<List<PatientFullInfoDto>> Handle(ListAllPatients request, CancellationToken cancellationToken)
        {
            var patients = await _patientRepository.GetAllAsync();
            var patientDtos = _mapper.Map<List<PatientFullInfoDto>>(patients);
            return await Task.FromResult(patientDtos);
        }
    }
}
