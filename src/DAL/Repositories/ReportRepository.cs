using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    /// <summary>
    /// Class repository for work with reports.
    /// </summary>
    public class ReportRepository : RepositoryBase<Report>, IReportRepository
    {
        public ReportRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        /// <summary>
        /// Method for get all reports from db.
        /// </summary>
        /// <param name="trackChanges">parameter that help in tracking changes.</param>
        /// <returns>list of all articles.</returns>
        public async Task<IEnumerable<Report>> GetAllReports(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(c => c.ReportDescription)
                .ToListAsync();

        /// <summary>
        /// Method for get report by id from db.
        /// </summary>
        /// <param name="reportId">id of report.</param>
        /// <returns>get report.</returns>
        public async Task<Report> GetReportById(int reportId)
        {
            return await FindByCondition(x => x.ReportId.Equals(reportId))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method for get report with details from db.
        /// </summary>
        /// <param name="reportId">id of report.</param>
        /// <returns>get report with details.</returns>
        public Report GetReportWithDetails(int reportId)
        {
            return FindByCondition(x => x.ReportId.Equals(reportId)).FirstOrDefault();
        }

        /// <summary>
        /// Method for create report in db.
        /// </summary>
        /// <param name="report">new report.</param>
        public void CreateReport(Report report)
        {
            Create(report);
        }

        /// <summary>
        /// Method for update report in db.
        /// </summary>
        /// <param name="report">updated report.</param>
        public void UpdateReport(Report report)
        {
            Update(report);
        }

        /// <summary>
        /// Method for deleting report from db.
        /// </summary>
        /// <param name="report">deleted report.</param>
        public void DeleteReport(Report report)
        {
            Delete(report);
        }

        /// <summary>
        /// Method for searching reports in db.
        /// </summary>
        /// <param name="reportId">search report.</param>
        /// <returns>list of reports.</returns>
        public async Task<IEnumerable<Report>> WhereIsReport(int reportId)
        {
            return await FindByCondition(i => i.Issue.IssueId == reportId)
                .OrderByDescending(a => a.ReportId).ToListAsync();
        }

        /// <summary>
        /// Method for getting all hours for reports in db.
        /// </summary>
        /// <param name="taskId">id of issue.</param>
        /// <returns>total hours for report.</returns>
        public async Task<int> GetAllReportsHours(int taskId)
        {
            return (await GetAllReports(true)).Where(x => x.IssueId == taskId)
                .Sum(x => x.AssignmentDate.Hour * 60 + x.AssignmentDate.Minute);
        }
    }
}
