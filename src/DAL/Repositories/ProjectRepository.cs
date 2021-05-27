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
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Project>> GetAllProjects(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToListAsync();

        public async Task<Project> GetProjectById(int projectId)
        {
            return await FindByCondition(x => x.ProjectId.Equals(projectId))
                .FirstOrDefaultAsync();
        }

        public Project GetProjectWithDetails(int projectId)
        {
            return FindByCondition(owner => owner.ProjectId.Equals(projectId)).FirstOrDefault();
        }

        public void CreateProject(Project project)
        {
            Create(project);
        }

        public void UpdateProject(Project project)
        {
            Update(project);
        }

        public void DeleteProject(Project project)
        {
            Delete(project);
        }
    }
}
