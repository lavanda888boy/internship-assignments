using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Doctors.Commands
{
    public record EmployNewDoctor(string Name, string Surname, string Address, string PhoneNumber, 
        int DepartmentId, TimeSpan StartShift, TimeSpan EndShift, List<int> WeekDayIds) 
        : IRequest<DoctorDto>;

    public class EmployNewDoctorHandler : IRequestHandler<EmployNewDoctor, DoctorDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployNewDoctorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DoctorDto> Handle(EmployNewDoctor request, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(request.DepartmentId);
            if (department == null)
            {
                throw new NoEntityFoundException($"Cannot employ doctor to a non-existing department with id {request.DepartmentId}");
            }

            try
            {
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

                await _unitOfWork.BeginTransactionAsync();
                _unitOfWork.DoctorRepository.Add(doctor);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return await Task.FromResult(DoctorDto.FromDoctor(doctor));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
