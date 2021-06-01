using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    /// <summary>
    /// Project entity model.
    /// </summary>
    public class Project
    {
        public int ProjectId { get; set; }

        [StringLength(256, MinimumLength = 5)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Issue> Issues { get; set; }
    }
}
