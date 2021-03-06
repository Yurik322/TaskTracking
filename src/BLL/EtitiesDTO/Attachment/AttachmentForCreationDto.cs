using System;
using DAL.Entities.Enums;

namespace BLL.EtitiesDTO.Attachment
{
    /// <summary>
    /// Data transfer object for attachment creation model.
    /// </summary>
    public class AttachmentForCreationDto
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public FileType FileType { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IssueId { get; set; }
    }
}
