using System;
using System.Collections.Generic;
using System.Text;
using BLL.EtitiesDTO;

namespace BLL.Interfaces
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);

        //IEnumerable<CompanyDto> FindBooks(string searchName);
        //CompanyDto GetBook(int? id);
        //IEnumerable<CompanyDto> GetBooks(string category, string author);
        //IEnumerable<CompanyDto> GetBooks();
        //void DeleteBook(int id);
        //void Update(CompanyDto bookDTO);
        //void CreateBook(CompanyDto bookDTO);
    }
}
