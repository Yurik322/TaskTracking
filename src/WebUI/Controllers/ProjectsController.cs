using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EtitiesDTO;
using BLL.EtitiesDTO.Issue;
using BLL.EtitiesDTO.Project;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers
{
    [Route("api/projects")]
    [Authorize]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ILoggerManager _logger;

        public ProjectsController(IProjectService projectService, ILoggerManager logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        // GET: /projects
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            try
            {
                var claims = User.Claims;
                var projectsDto = await _projectService.GetAllProjects();

                return Ok(projectsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetProjects)} action {ex}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // GET: /projects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            try
            {
                var projectResult = await _projectService.GetProjectById(id);
                return Ok(projectResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetProjectsById action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // POST: /projects/create
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectForCreationDto project)
        {
            try
            {
                if (project == null)
                {
                    _logger.LogError("Projects object sent from client is null.");
                    return BadRequest("Projects object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid project object sent from client.");
                    return BadRequest("Invalid model object");
                }

                await _projectService.CreateProject(project);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateProjects action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // PUT: /projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectForCreationDto project)
        {
            try
            {
                if (project == null)
                {
                    _logger.LogError("Projects object sent from client is null.");
                    return BadRequest("Projects object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid project object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var projectEntity = await _projectService.GetProjectById(id);
                if (projectEntity == null)
                {
                    _logger.LogError($"Projects with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                await _projectService.UpdateProject(id, project);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateProjects action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // DELETE: /projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                var project = await _projectService.GetProjectById(id);
                if (project == null)
                {
                    _logger.LogError($"Project with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                await _projectService.DeleteProject(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteProject action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // GET: /projects/5/issues
        [HttpGet("{id}/[action]")]
        public async Task<IEnumerable<IssueDto>> Issues(int id)
        {
            return await _projectService.GetIssuesByProject(id);
        }
    }
}
