using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.EtitiesDTO.Project
{
    /// <summary>
    /// Data transfer object for project model.
    /// </summary>
    public class ProjectDto
    {
        public int ProjectId { get; set; }

        [StringLength(256, MinimumLength = 5)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
