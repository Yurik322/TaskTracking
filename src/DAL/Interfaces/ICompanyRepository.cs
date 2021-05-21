using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
        Company GetOwnerById(int companyId);
        //void Create(Company company);
        Company GetOwnerWithDetails(Guid ownerId);
        void CreateCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(Company company);
    }
}
