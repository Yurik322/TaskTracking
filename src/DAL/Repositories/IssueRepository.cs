﻿using System;
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
    public class IssueRepository : RepositoryBase<Issue>, IIssueRepository
    {
        public IssueRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Issue>> GetAllIssues(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(c=>c.Title)
                .ToListAsync();

        //TODO
        public async Task<int> GetIssueHours(int taskId)
        {
            var task = await GetIssueById(taskId);

            return task.UpdatedAt.Hour * 60 + task.UpdatedAt.Minute;
        }

        public async Task<Issue> GetIssueById(int issueId)
        {
            return await FindByCondition(x => x.IssueId.Equals(issueId))
                .FirstOrDefaultAsync();
        }

        public Issue GetIssueWithDetails(int issueId)
        {
            return FindByCondition(owner => owner.IssueId.Equals(issueId)).FirstOrDefault();
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

        public async Task<IEnumerable<Issue>> WhereIsIssue(int issueId)
        {
            return await FindByCondition(i => i.Project.ProjectId == issueId)
                .OrderByDescending(i => i.IssueId).ToListAsync();
        }
    }
}
