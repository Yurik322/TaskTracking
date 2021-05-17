using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
    }
}
