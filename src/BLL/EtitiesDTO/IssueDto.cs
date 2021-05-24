using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DAL.Entities;
using DAL.Entities.Enums;

namespace BLL.EtitiesDTO
{
    public class IssueDto
    {
        public int Id { get; set; }

        [StringLength(256, MinimumLength = 5)]
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskType TaskType { get; set; }
        public Priority Priority { get; set; }
        public Status StatusType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
