using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.EtitiesDTO.Report
{
    /// <summary>
    /// Data transfer object for report model.
    /// </summary>
    public class ReportDto
    {
        public int ReportId { get; set; }
        public int EmployeeId { get; set; }
        public int IssueId { get; set; }
        public DateTime AssignmentDate { get; set; }
        public string ReportDescription { get; set; }
    }
}
