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
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Company> GetAllCompanies(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToList();

        public Company GetCompanyById(int companyId)
        {
            return FindByCondition(x => x.Id.Equals(companyId))
                .FirstOrDefault();
        }

        public Company GetCompanyWithDetails(int companyId)
        {
            return FindByCondition(owner => owner.Id.Equals(companyId))
                .Include(ac => ac.Employees)
                .FirstOrDefault();
        }

        public void CreateCompany(Company company)
        {
            Create(company);
        }

        //TODO
        public void UpdateCompany(Company company)
        {
            Update(company);
        }

        public void DeleteCompany(Company company)
        {
            throw new NotImplementedException();
        }
    }
}
