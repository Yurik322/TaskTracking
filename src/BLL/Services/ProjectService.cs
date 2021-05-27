using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EtitiesDTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BLL.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<ProjectDto> GetAllProjects()
        {
            var companies =
                _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDto>>(_repository.Project.GetAllProjects(trackChanges: false));

            return _mapper.Map<IEnumerable<ProjectDto>>(companies);
        }

        public ProjectDto GetProjectById(int id)
        {
            var project = _repository.Project.GetProjectById(id);

            return _mapper.Map<ProjectDto>(project);
        }

        public async Task CreateProject(ProjectForCreationDto project)
        {
            var projectEntity = _mapper.Map<Project>(project);

            _repository.Project.CreateProject(projectEntity);
            await _repository.SaveAsync();
        }

        public async Task UpdateProject(int id, ProjectForCreationDto project)
        {
            var projectEntity = _repository.Project.GetProjectById(id);

            _mapper.Map(project, projectEntity);
            _repository.Project.UpdateProject(projectEntity);
            await _repository.SaveAsync();
        }

        public async Task DeleteProject(int id)
        {
            var projectEntity = _repository.Project.GetProjectById(id);

            _repository.Project.DeleteProject(projectEntity);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<IssueDto>> GetIssuesByProject(int id)
        {
            var issues = _mapper.Map<IEnumerable<Issue>, IEnumerable<IssueDto>>
                (await _repository.Issue.WhereIsIssue(id));

            return _mapper.Map<IEnumerable<IssueDto>>(issues);
        }
    }
}
