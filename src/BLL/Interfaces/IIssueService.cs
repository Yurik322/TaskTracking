using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.EtitiesDTO.Attachment;
using BLL.EtitiesDTO.Issue;
using BLL.EtitiesDTO.Report;

namespace BLL.Interfaces
{
    /// <summary>
    /// Interface for issue service.
    /// </summary>
    public interface IIssueService
    {
        Task<IEnumerable<IssueDto>> GetAllIssues();
        Task<IssueDto> GetIssueById(int id);
        Task CreateIssue(IssueForCreationDto issue);
        Task UpdateIssue(int id, IssueForCreationDto issue);
        Task DeleteIssue(int id);
        Task<IEnumerable<AttachmentDto>> GetAttachmentsByIssue(int id);
        Task<IEnumerable<ReportDto>> GetReportsByIssue(int id);
        Task<double> PercentageCompleted(int id);
    }
}
