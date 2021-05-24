using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BLL.EtitiesDTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

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
            var companies = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDto>>(_repository.Project.GetAllProjects(trackChanges: false));

            return _mapper.Map<IEnumerable<ProjectDto>>(companies);
        }

        public void AddAsync(ProjectDto model)
        {
            var newModel = _mapper.Map<ProjectDto, Project>(model);
            _repository.Project.Create(newModel);
            _repository.SaveAsync();
        }
    }
}
