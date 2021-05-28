using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.EtitiesDTO.Report
{
    public class ReportForCreationDto
    {
        public int EmployeeId { get; set; }
        public int IssueId { get; set; }
        public DateTime AssignmentDate { get; set; }
        public string ReportDescription { get; set; }
    }
}
