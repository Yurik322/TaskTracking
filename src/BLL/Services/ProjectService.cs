using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EtitiesDTO;
using BLL.EtitiesDTO.Issue;
using BLL.EtitiesDTO.Project;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BLL.Services
{
    /// <summary>
    /// Class for project services.
    /// </summary>
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for get all ProjectDto objects.
        /// </summary>
        /// <returns>collection of ProjectDto.</returns>
        public async Task<IEnumerable<ProjectDto>> GetAllProjects()
        {
            var companies =
                _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDto>>(await _repository.Project.GetAllProjects(trackChanges: false));

            return _mapper.Map<IEnumerable<ProjectDto>>(companies);
        }

        /// <summary>
        /// Method for get ProjectDto object by id.
        /// </summary>
        /// <param name="id">id of ProjectDto.</param>
        /// <returns>object of ProjectDto</returns>
        public async Task<ProjectDto> GetProjectById(int id)
        {
            var project = await _repository.Project.GetProjectById(id);

            return _mapper.Map<ProjectDto>(project);
        }

        /// <summary>
        /// Method for create ProjectForCreationDto.
        /// </summary>
        /// <param name="project">new project.</param>
        /// <returns>new object.</returns>
        public async Task CreateProject(ProjectForCreationDto project)
        {
            var projectEntity = _mapper.Map<Project>(project);

            _repository.Project.CreateProject(projectEntity);
            await _repository.SaveAsync();
        }

        /// <summary>
        /// Method for update ProjectForCreationDto.
        /// </summary>
        /// <param name="id">id of updated project.</param>
        /// <param name="project">updated project.</param>
        /// <returns>updated object.</returns>
        public async Task UpdateProject(int id, ProjectForCreationDto project)
        {
            var projectEntity = await _repository.Project.GetProjectById(id);

            _mapper.Map(project, projectEntity);
            _repository.Project.UpdateProject(projectEntity);
            await _repository.SaveAsync();
        }

        /// <summary>
        /// Method for deleting ProjectDto.
        /// </summary>
        /// <param name="id">id of project.</param>
        /// <returns>deleted object.</returns>
        public async Task DeleteProject(int id)
        {
            var projectEntity = await _repository.Project.GetProjectById(id);

            _repository.Project.DeleteProject(projectEntity);
            await _repository.SaveAsync();
        }

        /// <summary>
        /// Method for getting IssueDto by project.
        /// </summary>
        /// <param name="id">id of project.</param>
        /// <returns>collection of issues.</returns>
        public async Task<IEnumerable<IssueDto>> GetIssuesByProject(int id)
        {
            var issues = _mapper.Map<IEnumerable<Issue>, IEnumerable<IssueDto>>
                (await _repository.Issue.WhereIsIssue(id));

            return _mapper.Map<IEnumerable<IssueDto>>(issues);
        }
    }
}
