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
    [Route("api/project")]
    [Authorize]
    [ApiController]
    public class ProjectsController : Controller
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

        // GET: /project/projects
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
        [HttpGet("{id}", Name = "ProjectsById")]
        public IActionResult GetProjectsById(int id)
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

        // POST: /project/create
        [HttpPost]
        public async Task<IActionResult> CreateProjects([FromBody] ProjectForCreationDto project)
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

                return CreatedAtRoute("ProjectsById", new { id = createdProjects.Id }, createdProjects);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateProjects action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // PUT: /project/5/edit
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProjects(int id, [FromBody] ProjectForCreationDto project)
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

        // DELETE: /project/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
            //var project = _context.Projects.Where(i => i.ProjectsId == id).FirstOrDefault();
            //if (project == null)
            //    return NotFound();

            //try
            //{
            //    _context.Projects.Remove(project);
            //    _context.SaveChanges();
            //    return Ok();
            //}
            //catch (DbUpdateException)
            //{
            //    return BadRequest();
            //}
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
