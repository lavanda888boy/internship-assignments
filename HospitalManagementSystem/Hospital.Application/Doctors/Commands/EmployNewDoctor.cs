using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Doctors.Commands
{
    public record EmployNewDoctor(string Name, string Surname, string Address, string PhoneNumber, 
        int DepartmentId, TimeSpan StartShift, TimeSpan EndShift, List<int> WeekDayIds) 
        : IRequest<int>;

    public class EmployNewDoctorHandler : IRequestHandler<EmployNewDoctor, int>
    {
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<Department> _departmentRepository;

        public EmployNewDoctorHandler(IRepository<Doctor> doctorRepository, 
            IRepository<Department> departmentRepository)
        {
            _doctorRepository = doctorRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<int> Handle(EmployNewDoctor request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetByIdAsync(request.DepartmentId);
            if (department == null)
            {
                throw new NoEntityFoundException($"Cannot employ doctor to a non-existing department with id {request.DepartmentId}");
            }

            var schedule = new DoctorSchedule()
            {
                StartShift = request.StartShift,
                EndShift = request.EndShift,
                DoctorScheduleWeekDay = request.WeekDayIds.Select(id => new DoctorScheduleWeekDay
                {
                    WeekDayId = id,
                }).ToList()
            };

            var doctor = new Doctor
            {
                Name = request.Name,
                Surname = request.Surname,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Department = department,
                WorkingHours = schedule,
            };

            await _doctorRepository.AddAsync(doctor);

            return await Task.FromResult(doctor.Id);
        }
    }
}
