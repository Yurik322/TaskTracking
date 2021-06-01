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
    /// Class repository for work with issues.
    /// </summary>
    public class IssueRepository : RepositoryBase<Issue>, IIssueRepository
    {
        public IssueRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        /// <summary>
        /// Method for get all issues from db.
        /// </summary>
        /// <param name="trackChanges">parameter that help in tracking changes.</param>
        /// <returns>list of all articles.</returns>
        public async Task<IEnumerable<Issue>> GetAllIssues(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(c=>c.Title)
                .ToListAsync();

        /// <summary>
        /// Method for get issue by id from db.
        /// </summary>
        /// <param name="issueId">id of issue.</param>
        /// <returns>get issue.</returns>
        public async Task<Issue> GetIssueById(int issueId)
        {
            return await FindByCondition(x => x.IssueId.Equals(issueId))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method for get issue with details from db.
        /// </summary>
        /// <param name="issueId">id of issue.</param>
        /// <returns>get issue with details.</returns>
        public Issue GetIssueWithDetails(int issueId)
        {
            return FindByCondition(owner => owner.IssueId.Equals(issueId)).FirstOrDefault();
        }

        /// <summary>
        /// Method for create issue in db.
        /// </summary>
        /// <param name="issue">new issue.</param>
        public void CreateIssue(Issue issue)
        {
            Create(issue);
        }

        /// <summary>
        /// Method for update issue in db.
        /// </summary>
        /// <param name="issue">updated issue.</param>
        public void UpdateIssue(Issue issue)
        {
            Update(issue);
        }

        /// <summary>
        /// Method for deleting issue from db.
        /// </summary>
        /// <param name="issue">deleted issue.</param>
        public void DeleteIssue(Issue issue)
        {
            Delete(issue);
        }

        /// <summary>
        /// Method for searching issues in db.
        /// </summary>
        /// <param name="issueId">search issue.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Issue>> WhereIsIssue(int issueId)
        {
            return await FindByCondition(i => i.Project.ProjectId == issueId)
                .OrderByDescending(i => i.IssueId).ToListAsync();
        }

        /// <summary>
        /// Method for getting all hours for issues in db.
        /// </summary>
        /// <param name="taskId">id of issue.</param>
        /// <returns>total hours for issues.</returns>
        public async Task<int> GetIssueHours(int taskId)
        {
            var task = await GetIssueById(taskId);
            return task.UpdatedAt.Hour * 60 + task.UpdatedAt.Minute;
        }
    }
}
