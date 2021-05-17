using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryBase<IArticleRepository> ArticleRepository { get; }
        IRepositoryBase<ICompanyRepository> CompanyRepository { get; }
        IRepositoryBase<IEmployeeRepository> EmployeeRepository { get; }
        IRepositoryBase<IRepositoryManager> RepositoryManager { get; }
        Task SaveAsync();
    }
}
