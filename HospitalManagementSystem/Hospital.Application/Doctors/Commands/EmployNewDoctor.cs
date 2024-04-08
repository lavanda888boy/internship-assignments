using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Doctors.Commands
{
    public record EmployNewDoctor(int Id, string Name, string Surname, string Address, 
        string PhoneNumber, int DepartmentId, int WorkingHoursId, TimeSpan StartShift,
        TimeSpan EndShift, List<DayOfWeek> WeekDays) : IRequest<DoctorDto>;

    public class EmployNewDoctorHandler : IRequestHandler<EmployNewDoctor, DoctorDto>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployNewDoctorHandler(IDoctorRepository doctorRepository,
            IDepartmentRepository departmentRepository)
        {
            _doctorRepository = doctorRepository;
            _departmentRepository = departmentRepository;
        }

        public Task<DoctorDto> Handle(EmployNewDoctor request, CancellationToken cancellationToken)
        {
            var doctor = new Doctor
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
                    EndShift = request.EndShift,
                    WeekDays = request.WeekDays,
                }
            };

            var createdDoctor = _doctorRepository.Create(doctor);
            return Task.FromResult(DoctorDto.FromDoctor(createdDoctor));
        }
    }
}
