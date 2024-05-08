using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Doctors.Commands
{
    public record UpdateDoctorPersonalInfo(int Id, string Name, string Surname, string Address,
        string PhoneNumber, int DepartmentId, TimeSpan StartShift, TimeSpan EndShift, 
        List<int> WeekDayIds) : IRequest<int>;

    public class UpdateDoctorPersonalInfoHandler : IRequestHandler<UpdateDoctorPersonalInfo, int>
    {
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<Department> _departmentRepository;

        public UpdateDoctorPersonalInfoHandler(IRepository<Doctor> doctorRepository,
            IRepository<Department> departmentRepository)
        {
            _doctorRepository = doctorRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<int> Handle(UpdateDoctorPersonalInfo request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetByIdAsync(request.DepartmentId);
            if (department == null)
            {
                throw new NoEntityFoundException($"Cannot update doctor with a non-existing department with id {request.DepartmentId}");
            }

            var existingDoctor = await _doctorRepository.GetByIdAsync(request.Id);
            if (existingDoctor != null)
            {
                existingDoctor.Name = request.Name;
                existingDoctor.Surname = request.Surname;
                existingDoctor.Address = request.Address;
                existingDoctor.PhoneNumber = request.PhoneNumber;
                existingDoctor.Department = department;
                existingDoctor.WorkingHours.StartShift = request.StartShift;
                existingDoctor.WorkingHours.EndShift = request.EndShift;

                existingDoctor.WorkingHours.DoctorScheduleWeekDay = request.WeekDayIds.Select(id => new DoctorScheduleWeekDay
                {
                    WeekDayId = id,
                }).ToList();

                await _doctorRepository.UpdateAsync(existingDoctor);

                return await Task.FromResult(existingDoctor.Id);
            }
            else
            {
                throw new NoEntityFoundException($"Cannot update non-existing doctor with id {request.Id}");
            }
        }
    }
}
