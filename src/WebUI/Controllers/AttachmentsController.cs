using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EtitiesDTO;
using BLL.EtitiesDTO.Attachment;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Entities.Enums;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace WebUI.Controllers
{
    [Route("api/attachments")]
    [Authorize]
    [ApiController]
    public class AttachmentsController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IAttachmentService _attachmentService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        //TODO
        private readonly IUnitOfWork _repository;


        public AttachmentsController(IAttachmentService attachmentService, ILoggerManager logger, IMapper mapper, IUnitOfWork repository, IWebHostEnvironment env)
        {
            _attachmentService = attachmentService;
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _env = env;
        }

        // GET: /attachments/companies
        //[Authorize]
        [HttpGet]
        public IActionResult GetAttachments()
        {
            try
            {
                var claims = User.Claims;
                var companiesDto = _attachmentService.GetAllAttachments();

                return Ok(companiesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAttachments)} action {ex}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        //TODO
        // GET: /attachments/5
        [HttpGet("{id}")]
        public IActionResult GetAttachmentById(int id)
        {
            try
            {
                var attachment = _repository.Attachment.GetAttachmentById(id);
                if (attachment == null)
                {
                    _logger.LogError($"Attachment with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned attachment with id: {id}");

                    var attachmentResult = _mapper.Map<AttachmentDto>(attachment);
                    return Ok(attachmentResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAttachmentById action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // DELETE: /attachments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttachment(int id)
        {
            try
            {
                var attachment = _repository.Attachment.GetAttachmentById(id);
                if (attachment == null)
                {
                    _logger.LogError($"Attachment with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Attachment.DeleteAttachment(attachment);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteAttachment action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // TODO
        // POST: /attachments/upload/{issueId}
        [HttpPut("[action]/{issueId}")]
        public async Task<IActionResult> Upload(int issueId, IFormFile file)
        {
            // Get issue to map the attachment
            var issue = _repository.Issue.GetIssueById(issueId);
            if (issue == null)
                return NotFound();

            // Basic validation
            if (!IsValidFile(file))
                return BadRequest();

            // Save file
            var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" +
                            DateTime.Now.ToString("yyyyMMddHHmmss") +
                            Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_env.WebRootPath, fileName);
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
                // file.CopyTo(fs);
            }

            // Save file record to db
            Attachment attachment = new Attachment()
            {
                Name = Path.GetFileNameWithoutExtension(file.FileName),
                CreatedAt = DateTime.Now,
                Path = fileName,
                FileType = GetFileType(file),
                IssueId = issueId
            };
            _repository.Attachment.CreateAttachment(attachment);
            await _repository.SaveAsync();

            return Ok();
        }

        private FileType GetFileType(IFormFile file)
        {
            string[] allowedExtensions = { "png", "jpg", "jpeg" };
            var fileExtension = Path.GetExtension(file.FileName);
            if (allowedExtensions.Any(e => fileExtension.Contains(e)))
                return FileType.Image;

            // Anyway...
            return FileType.Document;
        }

        private bool IsValidFile(IFormFile file)
        {
            string[] allowedExtensions = { "png", "jpg", "jpeg", "pdf", "doc", "docx" };
            string fileExtension = Path.GetExtension(file.FileName);

            // Invalid file
            if (file == null || file.Length == 0)
                return false;

            // Maximum file size allowed: 500kb
            if (file.Length > 512000)
                return false;

            // Only the above file extensions are allowed
            if (!allowedExtensions.Any(e => fileExtension.Contains(e)))
                return false;

            return true;
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
