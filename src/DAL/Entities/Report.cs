using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    /// <summary>
    /// Report entity model.
    /// </summary>
    public class Report
    {
        public int ReportId { get; set; }

        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(Issue))]
        public int IssueId { get; set; }
        public DateTime AssignmentDate { get; set; }
        public string ReportDescription { get; set; }

        public Employee Employee { get; set; }
        public Issue Issue { get; set; }
    }
}
