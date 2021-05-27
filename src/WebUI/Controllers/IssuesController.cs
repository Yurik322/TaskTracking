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
    [Route("api/issues")]
    [Authorize]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IIssueService _issueService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        //TODO
        private readonly IUnitOfWork _repository;


        public IssuesController(IIssueService issueService, ILoggerManager logger, IMapper mapper, IUnitOfWork repository)
        {
            _issueService = issueService;
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        // GET: /issues
        //[Authorize]
        [HttpGet]
        public IActionResult GetIssues()
        {
            try
            {
                var claims = User.Claims;
                var issuesDto = _issueService.GetAllIssues();

                return Ok(issuesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetIssues)} action {ex}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // GET: /issues/5
        [HttpGet("{id}")]
        public IActionResult GetIssueById(int id)
        {
            try
            {
                var issue = _repository.Issue.GetIssueById(id);
                if (issue == null)
                {
                    _logger.LogError($"Issue with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned issue with id: {id}");

                    var issueResult = _mapper.Map<IssueDto>(issue);
                    return Ok(issueResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetIssueById action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // POST: /issue/create
        [HttpPost]
        public async Task<IActionResult> CreateIssue([FromBody] IssueForCreationDto issue)
        {
            try
            {
                if (issue == null)
                {
                    _logger.LogError("Issue object sent from client is null.");
                    return BadRequest("Issue object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid issue object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var issueEntity = _mapper.Map<Issue>(issue);

                _repository.Issue.CreateIssue(issueEntity);
                await _repository.SaveAsync();

                var createdIssue = _mapper.Map<IssueDto>(issueEntity);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateIssue action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // PUT: /issues/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIssue(int id, [FromBody] IssueForCreationDto issue)
        {
            try
            {
                if (issue == null)
                {
                    _logger.LogError("Issue object sent from client is null.");
                    return BadRequest("Issue object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid issue object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var issueEntity = _repository.Issue.GetIssueById(id);
                if (issueEntity == null)
                {
                    _logger.LogError($"Issue with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(issue, issueEntity);

                _repository.Issue.UpdateIssue(issueEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateIssue action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // DELETE: /issue/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssue(int id)
        {
            try
            {
                var issue = _repository.Issue.GetIssueById(id);
                if (issue == null)
                {
                    _logger.LogError($"Issue with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Issue.DeleteIssue(issue);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteIssue action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // TODO
        // GET: /issues/5/attachments
        [HttpGet("{id}/[action]")]
        public IEnumerable<Attachment> Attachments(int id)
        {
            return _repository.Attachment.WhereIsAttachment(id);
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
