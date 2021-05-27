using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class IssueRepository : RepositoryBase<Issue>, IIssueRepository
    {
        public IssueRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Issue> GetAllIssues(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(c=>c.Title)
                .ToList();

        public Issue GetIssueById(int issueId)
        {
            return FindByCondition(x => x.Id.Equals(issueId))
                .FirstOrDefault();
        }

        public Issue GetIssueWithDetails(int issueId)
        {
            return FindByCondition(owner => owner.Id.Equals(issueId)).FirstOrDefault();
        }

        public void CreateIssue(Issue issue)
        {
            Create(issue);
        }

        public void UpdateIssue(Issue issue)
        {
            Update(issue);
        }

        public void DeleteIssue(Issue issue)
        {
            Delete(issue);
        }

        public IEnumerable<Issue> WhereIsIssue(int issueId)
        {
            return FindByCondition(i => i.Project.Id == issueId)
                .OrderByDescending(i => i.Id).ToList();

            //return FindByCondition(owner => owner.Id.Equals(issueId)).FirstOrDefault();
            //return Issue.WhereIsIssue(i => i.Project.ProjectId == id)
            //    .OrderByDescending(i => i.IssueId)
            //    .ToList();
        }
    }
}
