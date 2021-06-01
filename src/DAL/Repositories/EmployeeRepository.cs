using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    /// <summary>
    /// Class repository for work with employees.
    /// </summary>
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        /// <summary>
        /// Method for get all employees from db.
        /// </summary>
        /// <param name="trackChanges">parameter that help in tracking changes.</param>
        /// <returns>list of all articles.</returns>
        public async Task<IEnumerable<Employee>> GetAllEmployees(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(c => c.Position)
                .ToListAsync();

        /// <summary>
        /// Method for get employee by id from db.
        /// </summary>
        /// <param name="employeeId">id of employee.</param>
        /// <returns>get employee.</returns>
        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            return await FindByCondition(x => x.EmployeeId.Equals(employeeId))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method for get employee with details from db.
        /// </summary>
        /// <param name="employeeId">id of employee.</param>
        /// <returns>get employee with details.</returns>
        public Employee GetEmployeeWithDetails(int employeeId)
        {
            return FindByCondition(owner => owner.EmployeeId.Equals(employeeId)).FirstOrDefault();
        }

        /// <summary>
        /// Method for create employee in db.
        /// </summary>
        /// <param name="employee">new employee.</param>
        public void CreateEmployee(Employee employee)
        {
            Create(employee);
        }

        /// <summary>
        /// Method for update employee in db.
        /// </summary>
        /// <param name="employee">updated employee.</param>
        public void UpdateEmployee(Employee employee)
        {
            Update(employee);
        }

        /// <summary>
        /// Method for deleting employee from db.
        /// </summary>
        /// <param name="employee">deleted employee.</param>
        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }
    }
}
