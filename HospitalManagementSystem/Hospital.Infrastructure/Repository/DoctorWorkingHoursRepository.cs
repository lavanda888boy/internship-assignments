using Hospital.Application.Abstractions;
using Hospital.Domain.Models;

namespace Hospital.Infrastructure.Repository
{
    public class DoctorWorkingHoursRepository : IRepository<DoctorWorkingHours>
    {
        private List<DoctorWorkingHours> _workingHours = new();

        public DoctorWorkingHours Create(DoctorWorkingHours workingHours)
        {
            _workingHours.Add(workingHours);
            return workingHours;
        }

        public bool Delete(DoctorWorkingHours workingHours)
        {
            return _workingHours.Remove(workingHours);
        }

        public List<DoctorWorkingHours> GetAll()
        {
            return _workingHours;
        }

        public DoctorWorkingHours GetById(int id)
        {
            return _workingHours.First(wh => wh.Id == id);
        }

        public List<DoctorWorkingHours>? SearchByProperty(Func<DoctorWorkingHours, bool> workingHoursPredicate)
        {
            return _workingHours.Where(workingHoursPredicate).ToList();
        }

        public bool Update(DoctorWorkingHours workingHours)
        {
            var existingWorkingHours = _workingHours.First(wh => wh.Id == workingHours.Id);
            if (existingWorkingHours != null)
            {
                existingWorkingHours.Doctor = workingHours.Doctor;
                existingWorkingHours.StartShift = workingHours.StartShift;
                existingWorkingHours.EndShift = workingHours.EndShift;

                return true;
            }

            return false;
        }
    }
}
