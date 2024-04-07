using Hospital.Application.Abstractions;
using Hospital.Domain.Models;

namespace Hospital.Infrastructure.Repository
{
    internal class DoctorRepository : IRepository<Doctor>
    {
        private List<Doctor> _doctors = new();

        public Doctor Create(Doctor doctor)
        {
            _doctors.Add(doctor);
            return doctor;
        }

        public bool Delete(int doctorId)
        {
            var doctorToRemove = GetById(doctorId);
            if (doctorToRemove is null)
            {
                return false;
            }

            _doctors.Remove(doctorToRemove);
            return true;
        }

        public List<Doctor> GetAll()
        {
            return _doctors;
        }

        public Doctor GetById(int id)
        {
            return _doctors.First(d => d.Id == id);
        }

        public List<Doctor> SearchByProperty(Func<Doctor, bool> doctorProperty)
        {
            return _doctors.Where(doctorProperty).ToList();
        }

        public int GetLastId()
        {
            return _doctors.Any() ? _doctors.Max(doctor => doctor.Id) : 0;
        }

        public bool Update(Doctor doctor)
        {
            var existingDoctor = GetById(doctor.Id);
            if (existingDoctor != null)
            {
                int index = _doctors.IndexOf(existingDoctor);
                _doctors[index] = doctor;
                return true;
            }
            return false;
        }
    }
}
