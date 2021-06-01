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
    /// <summary>
    /// Class for issue services.
    /// </summary>
    public class IssueService : IIssueService
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public IssueService(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for get all IssueDto objects.
        /// </summary>
        /// <returns>collection of IssueDto.</returns>
        public async Task<IEnumerable<IssueDto>> GetAllIssues()
        {
            var issues =
                _mapper.Map<IEnumerable<Issue>, IEnumerable<IssueDto>>(await _repository.Issue.GetAllIssues(trackChanges: false));

            return _mapper.Map<IEnumerable<IssueDto>>(issues);
        }

        /// <summary>
        /// Method for get IssueDto object by id.
        /// </summary>
        /// <param name="id">id of IssueDto.</param>
        /// <returns>object of IssueDto</returns>
        public async Task<IssueDto> GetIssueById(int id)
        {
            var issue = await _repository.Issue.GetIssueById(id);

            return _mapper.Map<IssueDto>(issue);
        }

        /// <summary>
        /// Method for create IssueForCreationDto.
        /// </summary>
        /// <param name="issue">new issue.</param>
        /// <returns>new object.</returns>
        public async Task CreateIssue(IssueForCreationDto issue)
        {
            var issueEntity = _mapper.Map<Issue>(issue);

            _repository.Issue.CreateIssue(issueEntity);
            await _repository.SaveAsync();
        }

        /// <summary>
        /// Method for update IssueForCreationDto.
        /// </summary>
        /// <param name="id">id of updated issue.</param>
        /// <param name="issue">updated issue.</param>
        /// <returns>updated object.</returns>
        public async Task UpdateIssue(int id, IssueForCreationDto issue)
        {
            var issueEntity = await _repository.Issue.GetIssueById(id);

            _mapper.Map(issue, issueEntity);
            _repository.Issue.UpdateIssue(issueEntity);
            await _repository.SaveAsync();
        }

        /// <summary>
        /// Method for deleting IssueDto.
        /// </summary>
        /// <param name="id">id of issue.</param>
        /// <returns>deleted object.</returns>
        public async Task DeleteIssue(int id)
        {
            var issueEntity = await _repository.Issue.GetIssueById(id);

            _repository.Issue.DeleteIssue(issueEntity);
            await _repository.SaveAsync();
        }

        /// <summary>
        /// Method for getting AttachmentDto by issue.
        /// </summary>
        /// <param name="id">id of issue.</param>
        /// <returns>collection of issues.</returns>
        public async Task<IEnumerable<AttachmentDto>> GetAttachmentsByIssue(int id)
        {
            var attachments = _mapper.Map<IEnumerable<Attachment>, IEnumerable<AttachmentDto>>
                (await _repository.Attachment.WhereIsAttachment(id));

            return _mapper.Map<IEnumerable<AttachmentDto>>(attachments);
        }

        /// <summary>
        /// Method for getting ReportDto by issue.
        /// </summary>
        /// <param name="id">id of issue.</param>
        /// <returns>collection of issues.</returns>
        public async Task<IEnumerable<ReportDto>> GetReportsByIssue(int id)
        {
            var reports = _mapper.Map<IEnumerable<Report>, IEnumerable<ReportDto>>
                (await _repository.Report.WhereIsReport(id));

            return _mapper.Map<IEnumerable<ReportDto>>(reports);
        }

        /// <summary>
        /// Method for getting percentage of execution by issue.
        /// </summary>
        /// <param name="id">id of issue.</param>
        /// <returns>percentage of execution.</returns>
        public async Task<double> PercentageCompleted(int id)
        {
            var duration = (double)(await _repository.Issue.GetIssueHours(id));
            var hours = (double)(await _repository.Report.GetAllReportsHours(id));
            return (duration/hours)*100;
        }
    }
}
