using Hospital.Application.Abstractions;
using Hospital.Domain.Models;

namespace Hospital.Infrastructure.Repository
{
    internal class DepartmentRepository : IRepository<Department>
    {
        private List<Department> _departments = new();

        public Department Create(Department department)
        {
            _departments.Add(department);
            return department;
        }

        public bool Delete(Department department)
        {
            return _departments.Remove(department);
        }

        public List<Department> GetAll()
        {
            return _departments;
        }

        public Department GetById(int id)
        {
            return _departments.Single(d => d.Id == id);
        }

        public List<Department> GetByProperty(Func<Department, bool> departmentPredicate)
        {
            return _departments.Where(departmentPredicate).ToList();
        }

        public int GetLastId()
        {
            return _departments.Any() ? _departments.Max(department => department.Id) : 0;
        }

        public Department? Update(Department department)
        {
            var existingDepartment = _departments.FirstOrDefault(d => d.Id == department.Id);
            if (existingDepartment != null)
            {
                existingDepartment.Name = department.Name;
            }
            return existingDepartment;
        }
    }
}
