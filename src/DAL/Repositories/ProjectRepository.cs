using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Project> GetAllProjects(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToList();

        public Project GetProjectById(int projectId)
        {
            return FindByCondition(x => x.Id.Equals(projectId))
                .FirstOrDefault();
        }

        public Project GetProjectWithDetails(int projectId)
        {
            return FindByCondition(owner => owner.Id.Equals(projectId)).FirstOrDefault();
        }

        public void CreateProject(Project project)
        {
            Create(project);
        }

        //TODO
        public void UpdateProject(Project project)
        {
            Update(project);
        }

        public void DeleteProject(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
