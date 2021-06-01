using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.EtitiesDTO;
using BLL.EtitiesDTO.Attachment;
using BLL.EtitiesDTO.Issue;
using Microsoft.AspNetCore.Http;

namespace BLL.Interfaces
{
    /// <summary>
    /// Interface for attachment service.
    /// </summary>
    public interface IAttachmentService
    {
        Task<IEnumerable<AttachmentDto>> GetAllAttachments();
        Task<AttachmentDto> GetAttachmentById(int id);
        Task CreateAttachment(AttachmentForCreationDto attachment);
        Task UpdateAttachment(int id, AttachmentForCreationDto attachment);
        Task DeleteAttachment(int id);
        Task CreateFile(int issueId, IFormFile file);
    }
}
