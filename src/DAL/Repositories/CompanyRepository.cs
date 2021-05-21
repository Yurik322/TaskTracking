using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

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

        public Company GetOwnerById(int companyId)
        {
            return FindByCondition(x => x.Id.Equals(companyId), trackChanges: false)
                .FirstOrDefault();
        }

        public Company GetOwnerWithDetails(Guid ownerId)
        {
            throw new NotImplementedException();
        }

        public void CreateCompany(Company company)
        {
            throw new NotImplementedException();
        }

        public void UpdateCompany(Company company)
        {
            throw new NotImplementedException();
        }

        public void DeleteCompany(Company company)
        {
            throw new NotImplementedException();
        }


        //TODO
        public void CreateAsync(Company company)
        {
            Create(company);
        }
    }
}
