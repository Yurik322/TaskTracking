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
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(c => c.Position)
                .ToListAsync();

        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            return await FindByCondition(x => x.EmployeeId.Equals(employeeId))
                .FirstOrDefaultAsync();
        }

        public Employee GetEmployeeWithDetails(int employeeId)
        {
            return FindByCondition(owner => owner.EmployeeId.Equals(employeeId)).FirstOrDefault();
        }

        public void CreateEmployee(Employee employee)
        {
            Create(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            Update(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }
    }
}
