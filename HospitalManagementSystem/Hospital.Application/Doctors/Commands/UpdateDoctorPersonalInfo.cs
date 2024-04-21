//using Hospital.Application.Abstractions;
//using Hospital.Application.Doctors.Responses;
//using Hospital.Application.Exceptions;
//using Hospital.Domain.Models;
//using MediatR;

//namespace Hospital.Application.Doctors.Commands
//{
//    public record UpdateDoctorPersonalInfo(int Id, string Name, string Surname, string Address,
//        string PhoneNumber, int DepartmentId, int WorkingHoursId, TimeSpan StartShift, 
//        TimeSpan EndShift, List<WeekDay> WeekDays) : IRequest<DoctorDto>;

//    public class UpdateDoctorPersonalInfoHandler : IRequestHandler<UpdateDoctorPersonalInfo, DoctorDto>
//    {
//        private readonly IDoctorRepository _doctorRepository;
//        private readonly IDepartmentRepository _departmentRepository;

//        public UpdateDoctorPersonalInfoHandler(IDoctorRepository doctorRepository,
//            IDepartmentRepository departmentRepository)
//        {
//            _doctorRepository = doctorRepository;
//            _departmentRepository = departmentRepository;
//        }

//        public Task<DoctorDto> Handle(UpdateDoctorPersonalInfo request, CancellationToken cancellationToken)
//        {
//            var existingDoctor = _doctorRepository.GetById(request.Id);
//            if (existingDoctor != null)
//            {
//                var updatedDoctor = new Doctor()
//                {
//                    Id = request.Id,
//                    Name = request.Name,
//                    Surname = request.Surname,
//                    Address = request.Address,
//                    PhoneNumber = request.PhoneNumber,
//                    Department = _departmentRepository.GetById(request.DepartmentId),
//                    WorkingHours = new DoctorWorkingHours()
//                    {
//                        Id = request.WorkingHoursId,
//                        StartShift = request.StartShift,
//                        EndShift = request.EndShift,
//                        WeekDays = request.WeekDays,
//                    },
//                };

//                updatedDoctor.AssignedPatients = existingDoctor.AssignedPatients;
//                _doctorRepository.Update(updatedDoctor);

//                return Task.FromResult(DoctorDto.FromDoctor(updatedDoctor));
//            }
//            else
//            {
//                throw new NoEntityFoundException($"Cannot update non-existing doctor with id {request.Id}");
//            }
//        }
//    }
//}
