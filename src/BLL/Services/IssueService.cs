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
    public class IssueService : IIssueService
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public IssueService(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<IssueDto> GetAllIssues()
        {
            var issues = _mapper.Map<IEnumerable<Issue>, IEnumerable<IssueDto>>(_repository.Issue.GetAllIssues(trackChanges: false));

            return _mapper.Map<IEnumerable<IssueDto>>(issues);
        }

        public void AddAsync(IssueDto model)
        {
            var newModel = _mapper.Map<IssueDto, Issue>(model);
            _repository.Issue.Create(newModel);
            _repository.SaveAsync();
        }
    }
}
