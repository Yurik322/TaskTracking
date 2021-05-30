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
    public class ReportRepository : RepositoryBase<Report>, IReportRepository
    {
        public ReportRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Report>> GetAllReports(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(c => c.ReportDescription)
                .ToListAsync();

        public async Task<Report> GetReportById(int reportId)
        {
            return await FindByCondition(x => x.ReportId.Equals(reportId))
                .FirstOrDefaultAsync();
        }

        public Report GetReportWithDetails(int reportId)
        {
            return FindByCondition(owner => owner.ReportId.Equals(reportId)).FirstOrDefault();
        }

        public void CreateReport(Report report)
        {
            Create(report);
        }

        public void UpdateReport(Report report)
        {
            Update(report);
        }

        public void DeleteReport(Report report)
        {
            Delete(report);
        }

        public async Task<IEnumerable<Report>> WhereIsReport(int reportId)
        {
            return await FindByCondition(i => i.Issue.IssueId == reportId)
                .OrderByDescending(a => a.ReportId).ToListAsync();
        }
    }
}
