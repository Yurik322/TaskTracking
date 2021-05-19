using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DAL.Entities.Enums;

namespace DAL.Entities
{
    public class Issue
    {
        [Column("IssueId")]
        public Guid Id { get; set; }

        [StringLength(256, MinimumLength = 5)]
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskType TaskType { get; set; }
        public Priority Priority { get; set; }
        public Status StatusType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Project Project { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }
}
