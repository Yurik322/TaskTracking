using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.EtitiesDTO.Issue;
using BLL.EtitiesDTO.Report;

namespace BLL.Interfaces
{
    /// <summary>
    /// Interface for report service.
    /// </summary>
    public interface IReportService
    {
        Task<IEnumerable<ReportDto>> GetAllReports();
        Task<ReportDto> GetReportById(int id);
        Task CreateReport(ReportForCreationDto report);
        Task UpdateReport(int id, ReportForCreationDto report);
        Task DeleteReport(int id);
    }
}
