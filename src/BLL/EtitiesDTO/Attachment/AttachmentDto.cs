using System;
using DAL.Entities.Enums;

namespace BLL.EtitiesDTO.Attachment
{
    /// <summary>
    /// Data transfer object for attachment model.
    /// </summary>
    public class AttachmentDto
    {
        public int AttachmentId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public FileType FileType { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IssueId { get; set; }
    }
}
