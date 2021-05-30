using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _repositoryContext;
        private IEmployeeRepository _employeeRepository;
        private IIssueRepository _issueRepository;
        private IProjectRepository _projectRepository;
        private IReportRepository _reportRepository;
        private IAttachmentRepository _attachmentRepository;

        public UnitOfWork(ApplicationDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);

                return _employeeRepository;
            }
        }

        public IIssueRepository Issue
        {
            get
            {
                if (_issueRepository == null)
                    _issueRepository = new IssueRepository(_repositoryContext);

                return _issueRepository;
            }
        }

        public IProjectRepository Project
        {
            get
            {
                if (_projectRepository == null)
                    _projectRepository = new ProjectRepository(_repositoryContext);

                return _projectRepository;
            }
        }
        
        public IReportRepository Report
        {
            get
            {
                if (_reportRepository == null)
                    _reportRepository = new ReportRepository(_repositoryContext);

                return _reportRepository;
            }
        }
        
        public IAttachmentRepository Attachment
        {
            get
            {
                if (_attachmentRepository == null)
                    _attachmentRepository = new AttachmentRepository(_repositoryContext);

                return _attachmentRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _repositoryContext.SaveChangesAsync();
        }
    }
}