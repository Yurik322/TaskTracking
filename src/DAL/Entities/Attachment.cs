using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DAL.Entities.Enums;

namespace DAL.Entities
{
    /// <summary>
    /// Attachment entity model.
    /// </summary>
    public class Attachment
    {
        public int AttachmentId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public FileType FileType { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IssueId { get; set; }

        public Issue Issue { get; set; }
    }
}
