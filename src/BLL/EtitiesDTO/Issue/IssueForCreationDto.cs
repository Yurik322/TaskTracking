using System;
using System.ComponentModel.DataAnnotations;
using DAL.Entities.Enums;

namespace BLL.EtitiesDTO.Issue
{
    /// <summary>
    /// Data transfer object for issue creation model.
    /// </summary>
    public class IssueForCreationDto
    {
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
