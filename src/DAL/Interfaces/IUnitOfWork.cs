using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        void Save();
    }
}
