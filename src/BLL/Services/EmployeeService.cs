using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EtitiesDTO.Employee;
using BLL.EtitiesDTO.Issue;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    /// <summary>
    /// Class for employee services.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for get all EmployeeDto objects.
        /// </summary>
        /// <returns>collection of EmployeeDto.</returns>
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            var companies =
                _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(await _repository.Employee.GetAllEmployees(trackChanges: false));

            return _mapper.Map<IEnumerable<EmployeeDto>>(companies);
        }

        /// <summary>
        /// Method for get EmployeeDto object by id.
        /// </summary>
        /// <param name="id">id of EmployeeDto.</param>
        /// <returns>object of EmployeeDto</returns>
        public async Task<EmployeeDto> GetEmployeeById(int id)
        {
            var employee = await _repository.Employee.GetEmployeeById(id);

            return _mapper.Map<EmployeeDto>(employee);
        }

        /// <summary>
        /// Method for create EmployeeForCreationDto.
        /// </summary>
        /// <param name="employee">new employee.</param>
        /// <returns>new object.</returns>
        public async Task CreateEmployee(EmployeeForCreationDto employee)
        {
            var employeeEntity = _mapper.Map<Employee>(employee);

            _repository.Employee.CreateEmployee(employeeEntity);
            await _repository.SaveAsync();
        }

        /// <summary>
        /// Method for update EmployeeForCreationDto.
        /// </summary>
        /// <param name="id">id of updated employee.</param>
        /// <param name="employee">updated employee.</param>
        /// <returns>updated object.</returns>
        public async Task UpdateEmployee(int id, EmployeeForCreationDto employee)
        {
            var employeeEntity = await _repository.Employee.GetEmployeeById(id);

            _mapper.Map(employee, employeeEntity);
            _repository.Employee.UpdateEmployee(employeeEntity);
            await _repository.SaveAsync();
        }

        /// <summary>
        /// Method for deleting EmployeeDto.
        /// </summary>
        /// <param name="id">id of employee.</param>
        /// <returns>deleted object.</returns>
        public async Task DeleteEmployee(int id)
        {
            var employeeEntity = await _repository.Employee.GetEmployeeById(id);

            _repository.Employee.DeleteEmployee(employeeEntity);
            await _repository.SaveAsync();
        }

        /// <summary>
        /// Method for getting IssueDto by employee.
        /// </summary>
        /// <param name="id">id of employee.</param>
        /// <returns>collection of issues.</returns>
        public async Task<IEnumerable<IssueDto>> GetIssuesByEmployee(int id)
        {
            var issues = _mapper.Map<IEnumerable<Issue>, IEnumerable<IssueDto>>
                (await _repository.Issue.WhereIsIssue(id));

            return _mapper.Map<IEnumerable<IssueDto>>(issues);
        }
    }
}
