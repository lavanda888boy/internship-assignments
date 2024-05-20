using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Common;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Patients.Queries
{
    public record ListAllPaginatedPatients(int PageNumber, int PageSize) : IRequest<PaginatedResult<PatientFullInfoDto>>;

    public class ListAllPaginatedPatientsHandler : IRequestHandler<ListAllPaginatedPatients, PaginatedResult<PatientFullInfoDto>>
    {
        private readonly IRepository<Patient> _patientRepository;
        private readonly IMapper _mapper;

        public ListAllPaginatedPatientsHandler(IRepository<Patient> patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<PatientFullInfoDto>> Handle(ListAllPaginatedPatients request, CancellationToken cancellationToken)
        {
            var paginatedPatients = await _patientRepository.GetAllPaginatedAsync(request.PageNumber, request.PageSize);
            return await Task.FromResult(new PaginatedResult<PatientFullInfoDto>
            {
                TotalItems = paginatedPatients.TotalItems,
                Items = _mapper.Map<List<PatientFullInfoDto>>(paginatedPatients.Items)
            });
        }
    }
}
