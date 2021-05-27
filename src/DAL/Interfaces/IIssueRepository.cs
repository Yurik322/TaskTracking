﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IIssueRepository : IRepositoryBase<Issue>
    {
        IEnumerable<Issue> GetAllIssues(bool trackChanges);
        Issue GetIssueById(int issueId);
        Issue GetIssueWithDetails(int issueId);
        void CreateIssue(Issue issue);
        void UpdateIssue(Issue issue);
        void DeleteIssue(Issue issue);
        //TODO
        Task<IEnumerable<Issue>> WhereIsIssue(int issueId);
    }
}
