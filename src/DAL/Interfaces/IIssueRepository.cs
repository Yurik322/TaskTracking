using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IIssueRepository : IRepositoryBase<Issue>
    {
        Task<IEnumerable<Issue>> GetAllIssues(bool trackChanges);
        Task<Issue> GetIssueById(int issueId);
        Issue GetIssueWithDetails(int issueId);
        void CreateIssue(Issue issue);
        void UpdateIssue(Issue issue);
        void DeleteIssue(Issue issue);
        Task<IEnumerable<Issue>> WhereIsIssue(int issueId);
        Task<int> GetIssueHours(int taskId);
    }
}
