using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IReportRepository : IRepositoryBase<Report>
    {
        Task<IEnumerable<Report>> GetAllReports(bool trackChanges);
        Task<Report> GetReportById(int projectId);
        Report GetReportWithDetails(int projectId);
        void CreateReport(Report project);
        void UpdateReport(Report project);
        void DeleteReport(Report project);
    }
}
