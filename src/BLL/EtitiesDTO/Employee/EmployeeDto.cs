using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.EtitiesDTO.Employee
{
    /// <summary>
    /// Data transfer object for employee model.
    /// </summary>
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Age is a required field.")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Position is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters.")]
        public string Position { get; set; }
        public string UserId { get; set; }
    }
}
