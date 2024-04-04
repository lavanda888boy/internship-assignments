using Hospital.Application.Abstractions;
using Hospital.Domain.Models;

namespace Hospital.Infrastructure
{
    internal class DoctorRepository : IDoctorRepository
    {
        private List<Doctor> _doctors = new();

        public Doctor Create(Doctor doctor)
        {
            _doctors.Add(doctor);
            return doctor;
        }

        public bool Delete(Doctor doctor)
        {
            return _doctors.Remove(doctor);
        }

        public List<Doctor> GetAll()
        {
            return _doctors;
        }

        public List<Doctor> GetByProperty(Func<Doctor, bool> doctorProperty)
        {
            return _doctors.Where(doctorProperty).ToList();
        }

        public int GetLastId()
        {
            return _doctors.Any() ? _doctors.Max(doctor => doctor.Id) : 0;
        }

        public Doctor? Update(Doctor doctor)
        {
            var existingDoctor = _doctors.FirstOrDefault(d => d.Id == doctor.Id);
            if (existingDoctor != null)
            {
                existingDoctor.Name = doctor.Name;
                existingDoctor.Surname = doctor.Surname;
                existingDoctor.Address = doctor.Address;
                existingDoctor.PhoneNumber = doctor.PhoneNumber;
                existingDoctor.Department = doctor.Department;
            }
            return existingDoctor;
        }
    }
}
