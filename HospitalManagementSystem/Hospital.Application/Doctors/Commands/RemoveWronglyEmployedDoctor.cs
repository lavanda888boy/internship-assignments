using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Doctors.Commands
{
    public record RemoveWronglyEmployedDoctor(int DoctorId) : IRequest<int>;

    public class RemoveWronglyEmployedDoctorHandler : IRequestHandler<RemoveWronglyEmployedDoctor, int>
    {
        private readonly IRepository<Doctor> _doctorRepository;

        public RemoveWronglyEmployedDoctorHandler(IRepository<Doctor> doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<int> Handle(RemoveWronglyEmployedDoctor request, CancellationToken cancellationToken)
        {
            var doctorToDelete = await _doctorRepository.GetByIdAsync(request.DoctorId);

            if (doctorToDelete == null)
            {
                throw new NoEntityFoundException($"There is no doctor with id = {request.DoctorId} to delete");
            }

            await _doctorRepository.DeleteAsync(doctorToDelete);
            return await Task.FromResult(doctorToDelete.Id);
        }
    }
}
