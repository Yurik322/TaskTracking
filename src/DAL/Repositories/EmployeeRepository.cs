using System;
using System.Collections.Generic;
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
    }
}
