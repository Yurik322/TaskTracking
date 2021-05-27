using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.EtitiesDTO;

namespace BLL.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<ProjectDto> GetAllProjects();
        public ProjectDto GetProjectById(int id);
        public Task CreateProject(ProjectForCreationDto project);
        public Task UpdateProject(int id, ProjectForCreationDto project);
        public Task DeleteProject(int id);
        public Task<IEnumerable<IssueDto>> GetIssuesByProject(int id);
    }
}
