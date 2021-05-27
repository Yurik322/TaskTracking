using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.EtitiesDTO;
using BLL.EtitiesDTO.Issue;
using BLL.EtitiesDTO.Project;

namespace BLL.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjects();
        Task<ProjectDto> GetProjectById(int id);
        Task CreateProject(ProjectForCreationDto project);
        Task UpdateProject(int id, ProjectForCreationDto project);
        Task DeleteProject(int id);
        Task<IEnumerable<IssueDto>> GetIssuesByProject(int id);
    }
}
