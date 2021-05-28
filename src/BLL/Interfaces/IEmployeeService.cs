using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.EtitiesDTO.Employee;

namespace BLL.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployees();
        Task<EmployeeDto> GetEmployeeById(int id);
        Task CreateEmployee(EmployeeForCreationDto employee);
        Task UpdateEmployee(int id, EmployeeForCreationDto employee);
        Task DeleteEmployee(int id);
    }
}
