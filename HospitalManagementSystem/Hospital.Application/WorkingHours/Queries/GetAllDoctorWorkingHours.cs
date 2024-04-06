using Hospital.Application.Abstractions;
using Hospital.Application.WorkingHours.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.WorkingHours.Queries
{
    public record GetAllDoctorWorkingHours() : IRequest<List<DoctorWorkingHoursDto>>;

    public class GetAllDoctorWorkingHoursHandler 
        : IRequestHandler<GetAllDoctorWorkingHours, List<DoctorWorkingHoursDto>>
    {
        private readonly IRepository<DoctorWorkingHours> _workingHoursRepository;

        public GetAllDoctorWorkingHoursHandler(IRepository<DoctorWorkingHours> workingHoursRepository)
        {
            _workingHoursRepository = workingHoursRepository;
        }

        public Task<List<DoctorWorkingHoursDto>> Handle(GetAllDoctorWorkingHours request,
            CancellationToken cancellationToken)
        {
            var workingHours = _workingHoursRepository.GetAll();
            return Task.FromResult(workingHours.Select(DoctorWorkingHoursDto.FromDoctorWorkingHours).ToList());
        }
    }
}
