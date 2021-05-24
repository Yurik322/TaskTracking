using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Employee> GetAllEmployees(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToList();

        public Employee GetEmployeeById(int employeeId)
        {
            return FindByCondition(x => x.Id.Equals(employeeId))
                .FirstOrDefault();
        }

        public Employee GetEmployeeWithDetails(int employeeId)
        {
            return FindByCondition(owner => owner.Id.Equals(employeeId))
                .FirstOrDefault();
        }

        public void CreateEmployee(Employee employee)
        {
            Create(employee);
        }

        //TODO
        public void UpdateEmployee(Employee employee)
        {
            Update(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
