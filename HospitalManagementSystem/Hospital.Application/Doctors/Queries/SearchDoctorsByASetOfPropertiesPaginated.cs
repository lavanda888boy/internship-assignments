using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Common;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;
using System.Linq.Expressions;

namespace Hospital.Application.Doctors.Queries
{
    public record SearchDoctorsByASetOfPropertiesPaginated(int PageNumber, int PageSize, string? Name, string? Surname, string? Address,
        string? PhoneNumber, string? DepartmentName) : IRequest<PaginatedResult<DoctorFullInfoDto>>;

    public class SearchDoctorsByASetOfPropertiesPaginatedHandler 
        : IRequestHandler<SearchDoctorsByASetOfPropertiesPaginated, PaginatedResult<DoctorFullInfoDto>>
    {
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IMapper _mapper;

        public SearchDoctorsByASetOfPropertiesPaginatedHandler(IRepository<Doctor> doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<DoctorFullInfoDto>> Handle(SearchDoctorsByASetOfPropertiesPaginated request,
            CancellationToken cancellationToken)
        {
            Expression<Func<Doctor, bool>> predicate = d =>
                (string.IsNullOrEmpty(request.Name) || d.Name == request.Name) &&
                (string.IsNullOrEmpty(request.Surname) || d.Surname == request.Surname) &&
                (string.IsNullOrEmpty(request.Address) || d.Address == request.Address) &&
                (string.IsNullOrEmpty(request.PhoneNumber) || d.PhoneNumber == request.PhoneNumber) &&
                (string.IsNullOrEmpty(request.DepartmentName) || d.Department.Name == request.DepartmentName);

            var paginatedDoctors = await _doctorRepository.SearchByPropertyPaginatedAsync(predicate, request.PageNumber, request.PageSize);

            if (paginatedDoctors.Items.Count == 0)
            {
                throw new NoEntityFoundException("No doctors with such properties exist");
            }

            return await Task.FromResult(new PaginatedResult<DoctorFullInfoDto>
            {
                TotalItems = paginatedDoctors.TotalItems,
                Items = _mapper.Map<List<DoctorFullInfoDto>>(paginatedDoctors.Items)
            });
        }
    }
}
