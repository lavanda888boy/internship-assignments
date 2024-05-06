﻿using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;
using System.Linq.Expressions;

namespace Hospital.Application.Doctors.Queries
{
    public record SearchDoctorsByASetOfProperties(DoctorFilters DoctorFilters) : IRequest<List<DoctorDto>>;

    public class SearchDoctorsByASetOfPropertiesHandler : IRequestHandler<SearchDoctorsByASetOfProperties, List<DoctorDto>>
    {
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IMapper _mapper;

        public SearchDoctorsByASetOfPropertiesHandler(IRepository<Doctor> doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<List<DoctorDto>> Handle(SearchDoctorsByASetOfProperties request, CancellationToken cancellationToken)
        {
            Expression<Func<Doctor, bool>> predicate = d =>
                (string.IsNullOrEmpty(request.DoctorFilters.Name) || d.Name == request.DoctorFilters.Name) &&
                (string.IsNullOrEmpty(request.DoctorFilters.Surname) || d.Surname == request.DoctorFilters.Surname) &&
                (string.IsNullOrEmpty(request.DoctorFilters.Address) || d.Address == request.DoctorFilters.Address) &&
                (string.IsNullOrEmpty(request.DoctorFilters.PhoneNumber) || d.PhoneNumber == request.DoctorFilters.PhoneNumber) &&
                (string.IsNullOrEmpty(request.DoctorFilters.DepartmentName) || d.Department.Name == request.DoctorFilters.DepartmentName);

            var doctors = await _doctorRepository.SearchByPropertyAsync(predicate);

            if (doctors.Count == 0)
            {
                throw new NoEntityFoundException("No doctors with such properties exist");
            }

            var doctorDtos = _mapper.Map<List<DoctorDto>>(doctors);
            return await Task.FromResult(doctorDtos);
        }
    }
}
