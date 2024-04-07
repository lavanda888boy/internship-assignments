using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Doctors.Commands
{
    public record CreateDoctor(int Id, string Name, string Surname, string Address, 
        string PhoneNumber, int DepartmentId, int WorkingHoursId, TimeSpan StartShift,
        TimeSpan EndShift) : IRequest<DoctorDto>;

    public class CreateDoctorHandler : IRequestHandler<CreateDoctor, DoctorDto>
    {
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<Department> _departmentRepository;

        public CreateDoctorHandler(IRepository<Doctor> doctorRepository,
            IRepository<Department> departmentRepository)
        {
            _doctorRepository = doctorRepository;
            _departmentRepository = departmentRepository;
        }

        public Task<DoctorDto> Handle(CreateDoctor request, CancellationToken cancellationToken)
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
                    EndShift = request.EndShift
                }
            };

            var createdDoctor = _doctorRepository.Create(doctor);
            return Task.FromResult(DoctorDto.FromDoctor(createdDoctor));
        }
    }
}
