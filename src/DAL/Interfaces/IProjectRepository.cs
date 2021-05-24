using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IProjectRepository : IRepositoryBase<Project>
    {
        IEnumerable<Project> GetAllProjects(bool trackChanges);
        Project GetProjectById(int projectId);
        Project GetProjectWithDetails(int projectId);
        void CreateProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(Project project);
    }
}
