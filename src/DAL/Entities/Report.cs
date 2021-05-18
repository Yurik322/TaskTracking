using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    public class Report
    {
        [Column("ReportId")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Employee))]
        public Guid EmployeeId { get; set; }

        [ForeignKey(nameof(Task))]
        public Guid TaskId { get; set; }
        public DateTime AssignmentDate { get; set; }
        public string ReportDescription { get; set; }

        public Employee Employee { get; set; }
        public Task Task { get; set; }
    }
}
