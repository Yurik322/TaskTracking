using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EtitiesDTO;
using BLL.EtitiesDTO.Attachment;
using BLL.EtitiesDTO.Issue;
using BLL.EtitiesDTO.Report;
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

        public async Task<IEnumerable<IssueDto>> GetAllIssues()
        {
            var issues =
                _mapper.Map<IEnumerable<Issue>, IEnumerable<IssueDto>>(await _repository.Issue.GetAllIssues(trackChanges: false));

            return _mapper.Map<IEnumerable<IssueDto>>(issues);
        }

        public async Task<IssueDto> GetIssueById(int id)
        {
            var issue = await _repository.Issue.GetIssueById(id);

            return _mapper.Map<IssueDto>(issue);
        }

        public async Task CreateIssue(IssueForCreationDto issue)
        {
            var issueEntity = _mapper.Map<Issue>(issue);

            _repository.Issue.CreateIssue(issueEntity);
            await _repository.SaveAsync();
        }

        public async Task UpdateIssue(int id, IssueForCreationDto issue)
        {
            var issueEntity = await _repository.Issue.GetIssueById(id);

            _mapper.Map(issue, issueEntity);
            _repository.Issue.UpdateIssue(issueEntity);
            await _repository.SaveAsync();
        }

        public async Task DeleteIssue(int id)
        {
            var issueEntity = await _repository.Issue.GetIssueById(id);

            _repository.Issue.DeleteIssue(issueEntity);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<AttachmentDto>> GetAttachmentsByIssue(int id)
        {
            var attachments = _mapper.Map<IEnumerable<Attachment>, IEnumerable<AttachmentDto>>
                (await _repository.Attachment.WhereIsAttachment(id));

            return _mapper.Map<IEnumerable<AttachmentDto>>(attachments);
        }

        public async Task<IEnumerable<ReportDto>> GetReportsByIssue(int id)
        {
            var reports = _mapper.Map<IEnumerable<Report>, IEnumerable<ReportDto>>
                (await _repository.Report.WhereIsReport(id));

            return _mapper.Map<IEnumerable<ReportDto>>(reports);
        }
    }
}
