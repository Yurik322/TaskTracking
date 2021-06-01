using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface of repository that work with projects.
    /// </summary>
    public interface IProjectRepository : IRepositoryBase<Project>
    {
        Task<IEnumerable<Project>> GetAllProjects(bool trackChanges);
        Task<Project> GetProjectById(int projectId);
        Project GetProjectWithDetails(int projectId);
        void CreateProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(Project project);
    }
}
