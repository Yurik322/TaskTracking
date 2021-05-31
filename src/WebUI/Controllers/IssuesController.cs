using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EtitiesDTO;
using BLL.EtitiesDTO.Attachment;
using BLL.EtitiesDTO.Issue;
using BLL.EtitiesDTO.Report;
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

        public IssuesController(IIssueService issueService, ILoggerManager logger)
        {
            _issueService = issueService;
            _logger = logger;
        }

        // GET: /issues
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetIssues()
        {
            try
            {
                var claims = User.Claims;
                var issuesDto = await _issueService.GetAllIssues();

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
        public async Task<IActionResult> GetIssueById(int id)
        {
            try
            {
                var issueResult = await _issueService.GetIssueById(id);
                return Ok(issueResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetIssuesById action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // POST: /issues/create
        [HttpPost]
        public async Task<IActionResult> CreateIssue([FromBody] IssueForCreationDto issue)
        {
            try
            {
                if (issue == null)
                {
                    _logger.LogError("Issues object sent from client is null.");
                    return BadRequest("Issues object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid issue object sent from client.");
                    return BadRequest("Invalid model object");
                }

                await _issueService.CreateIssue(issue);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateIssues action: {ex.Message}");
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
                    _logger.LogError("Issues object sent from client is null.");
                    return BadRequest("Issues object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid issue object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var issueEntity = await _issueService.GetIssueById(id);
                if (issueEntity == null)
                {
                    _logger.LogError($"Issues with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                await _issueService.UpdateIssue(id, issue);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateIssues action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // DELETE: /issues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssue(int id)
        {
            try
            {
                var issue = await _issueService.GetIssueById(id);
                if (issue == null)
                {
                    _logger.LogError($"Issue with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                await _issueService.DeleteIssue(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteIssue action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // GET: /issues/5/reports
        [HttpGet("{id}/[action]")]
        public async Task<IEnumerable<ReportDto>> Reports(int id)
        {
            return await _issueService.GetReportsByIssue(id);
        }

        // GET: /issues/5/attachments
        [HttpGet("{id}/[action]")]
        public async Task<IEnumerable<AttachmentDto>> Attachments(int id)
        {
            return await _issueService.GetAttachmentsByIssue(id);
        }
    }
}
