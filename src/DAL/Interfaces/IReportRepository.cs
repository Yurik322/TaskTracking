using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IReportRepository : IRepositoryBase<Report>
    {
        IEnumerable<Report> GetAllReports(bool trackChanges);
        Report GetReportById(int reportId);
        Report GetReportWithDetails(int reportId);
        void CreateReport(Report report);
        void UpdateReport(Report report);
        void DeleteReport(Report report);
    }
}
