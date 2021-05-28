using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.EtitiesDTO.Attachment;
using BLL.EtitiesDTO.Issue;

namespace BLL.Interfaces
{
    public interface IIssueService
    {
        Task<IEnumerable<IssueDto>> GetAllIssues();
        Task<IssueDto> GetIssueById(int id);
        Task CreateIssue(IssueForCreationDto issue);
        Task UpdateIssue(int id, IssueForCreationDto issue);
        Task DeleteIssue(int id);
        Task<IEnumerable<AttachmentDto>> GetAttachmentsByIssue(int id);
    }
}
