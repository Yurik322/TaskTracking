using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface of repository that work with reports.
    /// </summary>
    public interface IReportRepository : IRepositoryBase<Report>
    {
        Task<IEnumerable<Report>> GetAllReports(bool trackChanges);
        Task<Report> GetReportById(int reportId);
        Report GetReportWithDetails(int reportId);
        void CreateReport(Report report);
        void UpdateReport(Report report);
        void DeleteReport(Report report);
        Task<IEnumerable<Report>> WhereIsReport(int reportId);
        Task<int> GetAllReportsHours(int taskId);
    }
}
