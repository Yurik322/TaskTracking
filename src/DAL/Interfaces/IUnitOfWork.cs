using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employee { get; }
        IIssueRepository Issue { get; }
        IProjectRepository Project { get; }
        IReportRepository Report { get; }
        IAttachmentRepository Attachment { get; }
        Task SaveAsync();
    }
}
