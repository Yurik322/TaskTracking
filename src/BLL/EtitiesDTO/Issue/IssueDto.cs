using System;
using System.ComponentModel.DataAnnotations;
using DAL.Entities.Enums;

namespace BLL.EtitiesDTO.Issue
{
    public class IssueDto
    {
        public int IssueId { get; set; }

        [StringLength(256, MinimumLength = 5)]
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskType TaskType { get; set; }
        public Priority Priority { get; set; }
        public Status StatusType { get; set; }
        public int ProjectId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
