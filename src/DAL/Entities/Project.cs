using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    public class Project
    {
        [Column("ProjectId")]
        public Guid Id { get; set; }

        [StringLength(256, MinimumLength = 5)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
