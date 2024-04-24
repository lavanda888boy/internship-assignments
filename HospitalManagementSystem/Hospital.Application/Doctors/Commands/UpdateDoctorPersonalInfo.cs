using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Doctors.Commands
{
    public record UpdateDoctorPersonalInfo(int Id, string Name, string Surname, string Address,
        string PhoneNumber, int DepartmentId, TimeSpan StartShift, TimeSpan EndShift, int ScheduleId, 
        List<int> WeekDayIds) : IRequest<DoctorDto>;

    public class UpdateDoctorPersonalInfoHandler : IRequestHandler<UpdateDoctorPersonalInfo, DoctorDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDoctorPersonalInfoHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DoctorDto> Handle(UpdateDoctorPersonalInfo request, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(request.DepartmentId);
            if (department == null)
            {
                throw new NoEntityFoundException($"Cannot update doctor with a non-existing department with id {request.DepartmentId}");
            }

            var existingDoctor = await _unitOfWork.DoctorRepository.GetByIdAsync(request.Id);
            if (existingDoctor != null)
            {
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

                    await _unitOfWork.BeginTransactionAsync();
                    await _unitOfWork.DoctorRepository.UpdateAsync(existingDoctor);
                    await _unitOfWork.SaveAsync();
                    await _unitOfWork.CommitTransactionAsync();

                    return await Task.FromResult(DoctorDto.FromDoctor(existingDoctor));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await _unitOfWork.RollbackTransactionAsync();
                    throw;
                }
            }
            else
            {
                throw new NoEntityFoundException($"Cannot update non-existing doctor with id {request.Id}");
            }
        }
    }
}
