using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Common;
using Hospital.Application.Doctors.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Doctors.Queries
{
    public record ListAllPaginatedDoctors(int PageNumber, int PageSize) : IRequest<PaginatedResult<DoctorFullInfoDto>>;

    public class ListAllPaginatedDoctorsHandler : IRequestHandler<ListAllPaginatedDoctors, PaginatedResult<DoctorFullInfoDto>>
    {
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IMapper _mapper;

        public ListAllPaginatedDoctorsHandler(IRepository<Doctor> doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<DoctorFullInfoDto>> Handle(ListAllPaginatedDoctors request, CancellationToken cancellationToken)
        {
            var paginatedDoctors = await _doctorRepository.GetAllPaginatedAsync(request.PageNumber, request.PageSize);
            return await Task.FromResult(new PaginatedResult<DoctorFullInfoDto>
            {
                TotalItems = paginatedDoctors.TotalItems,
                Items = _mapper.Map<List<DoctorFullInfoDto>>(paginatedDoctors.TotalItems)
            });
        }
    }
}
