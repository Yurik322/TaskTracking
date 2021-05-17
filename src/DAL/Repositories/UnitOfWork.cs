using System;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
   public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private IRepositoryBase<ArticleRepository> _articleRepository;
        private IRepositoryBase<CompanyRepository> _companyRepository;
        private IRepositoryBase<EmployeeRepository> _employeeRepository;
        private IRepositoryBase<RepositoryManager> _repositoryManager;

        public UnitOfWork(DbContextOptions<ApplicationDbContext> options)
        {
            _db = new ApplicationDbContext(options);
        }

        public IRepositoryBase<IArticleRepository> ArticleRepository { get; }
        public IRepositoryBase<ICompanyRepository> CompanyRepository { get; }
        public IRepositoryBase<IEmployeeRepository> EmployeeRepository { get; }
        public IRepositoryBase<IRepositoryManager> RepositoryManager { get; }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (this._disposed)
                return;
            if (disposing)
            {
                _db.Dispose();
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
