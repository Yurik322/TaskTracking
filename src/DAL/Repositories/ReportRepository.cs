using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ReportRepository : RepositoryBase<Report>, IReportRepository
    {
        public ReportRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Report> GetAllReports(bool trackChanges) =>
            FindAll(trackChanges).ToList();

        public Report GetReportById(int reportId)
        {
            return FindByCondition(x => x.Id.Equals(reportId))
                .FirstOrDefault();
        }

        public Report GetReportWithDetails(int reportId)
        {
            return FindByCondition(owner => owner.Id.Equals(reportId)).FirstOrDefault();
        }

        public void CreateReport(Report report)
        {
            Create(report);
        }

        //TODO
        public void UpdateReport(Report report)
        {
            Update(report);
        }

        public void DeleteReport(Report report)
        {
            throw new NotImplementedException();
        }
    }
}
