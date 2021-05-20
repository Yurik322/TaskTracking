﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.EtitiesDTO;

namespace BLL.Interfaces
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
        Task AddAsync(CompanyDto model);


        //IEnumerable<CompanyDto> FindBooks(string searchName);
        //CompanyDto GetBook(int? id);
        //IEnumerable<CompanyDto> GetBooks(string category, string author);
        //IEnumerable<CompanyDto> GetBooks();
        //void DeleteBook(int id);
        //void Update(CompanyDto bookDTO);
        //void CreateBook(CompanyDto bookDTO);
    }
}
