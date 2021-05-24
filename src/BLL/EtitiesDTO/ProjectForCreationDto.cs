using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.EtitiesDTO
{
    public class ProjectForCreationDto
    {

        [StringLength(256, MinimumLength = 5)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
