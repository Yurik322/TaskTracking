using System;
using System.Collections.Generic;
using System.Text;
using BLL.EtitiesDTO;

namespace BLL.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<ProjectDto> GetAllProjects();
        void AddAsync(ProjectDto model);


        //IEnumerable<ProjectDto> FindBooks(string searchName);
        //ProjectDto GetBook(int? id);
        //IEnumerable<ProjectDto> GetBooks(string category, string author);
        //IEnumerable<ProjectDto> GetBooks();
        //void DeleteBook(int id);
        //void Update(ProjectDto bookDTO);
        //void CreateBook(ProjectDto bookDTO);
    }
}
