using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EtitiesDTO;
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
        private readonly IMapper _mapper;

        //TODO
        private readonly IUnitOfWork _repository;


        public ProjectsController(IProjectService projectService, ILoggerManager logger, IMapper mapper, IUnitOfWork repository)
        {
            _projectService = projectService;
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        // GET: /projects
        //[Authorize]
        [HttpGet]
        public IActionResult GetProjects()
        {
            try
            {
                var claims = User.Claims;
                var projectsDto = _projectService.GetAllProjects();

                return Ok(projectsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetProjects)} action {ex}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        //TODO
        // GET: /projects/5
        [HttpGet("{id}")]
        public IActionResult GetProjectById(int id)
        {
            try
            {
                var project = _repository.Project.GetProjectById(id);
                if (project == null)
                {
                    _logger.LogError($"Projects with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned project with id: {id}");

                    var projectResult = _mapper.Map<ProjectDto>(project);
                    return Ok(projectResult);
                }
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

                var projectEntity = _mapper.Map<Project>(project);

                _repository.Project.CreateProject(projectEntity);
                await _repository.SaveAsync();

                var createdProjects = _mapper.Map<ProjectDto>(projectEntity);

                return Ok();
                //return CreatedAtRoute("ProjectsById", new { id = createdProjects.Id }, createdProjects);
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

                var projectEntity = _repository.Project.GetProjectById(id);
                if (projectEntity == null)
                {
                    _logger.LogError($"Projects with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(project, projectEntity);

                _repository.Project.UpdateProject(projectEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateProjects action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: /projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                var project = _repository.Project.GetProjectById(id);
                if (project == null)
                {
                    _logger.LogError($"Project with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Project.DeleteProject(project);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteProject action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //TODO
        // GET: /projects/5/issues
        [HttpGet("{id}/[action]")]
        public IEnumerable<Issue> Issues(int id)
        {
            return _repository.Issue.WhereIsIssue(id);
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
