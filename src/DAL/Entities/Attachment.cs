using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DAL.Entities.Enums;

namespace DAL.Entities
{
    public class Attachment
    {
        [Column("AttachmentId")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public FileType FileType { get; set; }
        public DateTime CreatedAt { get; set; }

        public Issue Issue { get; set; }
    }
}
