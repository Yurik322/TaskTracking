using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.EtitiesDTO.Employee;
using BLL.EtitiesDTO.Issue;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers
{
    /// <summary>
    /// Employees controller.
    /// </summary>
    [Route("api/employees")]
    [Authorize]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILoggerManager _logger;

        public EmployeesController(IEmployeeService employeeService, ILoggerManager logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        // GET: /employees
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var claims = User.Claims;
                var employeesDto = await _employeeService.GetAllEmployees();

                return Ok(employeesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetEmployees)} action {ex}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // GET: /employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employeeResult = await _employeeService.GetEmployeeById(id);
                return Ok(employeeResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEmployeesById action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // POST: /employees/create
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeForCreationDto employee)
        {
            try
            {
                if (employee == null)
                {
                    _logger.LogError("Employees object sent from client is null.");
                    return BadRequest("Employees object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid employee object sent from client.");
                    return BadRequest("Invalid model object");
                }

                await _employeeService.CreateEmployee(employee);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateEmployees action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // PUT: /employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeForCreationDto employee)
        {
            try
            {
                if (employee == null)
                {
                    _logger.LogError("Employees object sent from client is null.");
                    return BadRequest("Employees object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid employee object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var employeeEntity = await _employeeService.GetEmployeeById(id);
                if (employeeEntity == null)
                {
                    _logger.LogError($"Employees with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                await _employeeService.UpdateEmployee(id, employee);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateEmployees action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // DELETE: /employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeById(id);
                if (employee == null)
                {
                    _logger.LogError($"Employee with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                await _employeeService.DeleteEmployee(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        [HttpGet("Privacy")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Privacy()
        {
            var claims = User.Claims
                .Select(c => new { c.Type, c.Value })
                .ToList();

            return Ok(claims);
        }

    }
}
