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
    /// <summary>
    /// Class repository for work with projects.
    /// </summary>
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        /// <summary>
        /// Method for get all projects from db.
        /// </summary>
        /// <param name="trackChanges">parameter that help in tracking changes.</param>
        /// <returns>list of all articles.</returns>
        public async Task<IEnumerable<Project>> GetAllProjects(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToListAsync();

        /// <summary>
        /// Method for get project by id from db.
        /// </summary>
        /// <param name="projectId">id of project.</param>
        /// <returns>get project.</returns>
        public async Task<Project> GetProjectById(int projectId)
        {
            return await FindByCondition(x => x.ProjectId.Equals(projectId))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method for get project with details from db.
        /// </summary>
        /// <param name="projectId">id of project.</param>
        /// <returns>get project with details.</returns>
        public Project GetProjectWithDetails(int projectId)
        {
            return FindByCondition(owner => owner.ProjectId.Equals(projectId)).FirstOrDefault();
        }

        /// <summary>
        /// Method for create project in db.
        /// </summary>
        /// <param name="project">new project.</param>
        public void CreateProject(Project project)
        {
            Create(project);
        }

        /// <summary>
        /// Method for update project in db.
        /// </summary>
        /// <param name="project">updated project.</param>
        public void UpdateProject(Project project)
        {
            Update(project);
        }

        /// <summary>
        /// Method for deleting project from db.
        /// </summary>
        /// <param name="project">deleted project.</param>
        public void DeleteProject(Project project)
        {
            Delete(project);
        }
    }
}
