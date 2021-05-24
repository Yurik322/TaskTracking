using System;
using System.Collections.Generic;
using System.Text;
using BLL.EtitiesDTO;

namespace BLL.Interfaces
{
    public interface IIssueService
    {
        IEnumerable<IssueDto> GetAllIssues();
        void AddAsync(IssueDto model);


        //IEnumerable<IssueDto> FindBooks(string searchName);
        //IssueDto GetBook(int? id);
        //IEnumerable<IssueDto> GetBooks(string category, string author);
        //IEnumerable<IssueDto> GetBooks();
        //void DeleteBook(int id);
        //void Update(IssueDto bookDTO);
        //void CreateBook(IssueDto bookDTO);
    }
}
