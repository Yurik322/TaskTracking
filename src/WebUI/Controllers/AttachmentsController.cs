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
        //TODO
        private readonly IUnitOfWork _repository;

        public AttachmentsController(IAttachmentService attachmentService, ILoggerManager logger, IUnitOfWork repository, IWebHostEnvironment env)
        {
            _attachmentService = attachmentService;
            _logger = logger;
            _repository = repository;
            _env = env;
        }

        // GET: /attachments
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAttachments()
        {
            try
            {
                var claims = User.Claims;
                var attachmentsDto = await _attachmentService.GetAllAttachments();

                return Ok(attachmentsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAttachments)} action {ex}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // GET: /attachments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttachmentById(int id)
        {
            try
            {
                var attachmentResult = await _attachmentService.GetAttachmentById(id);
                return Ok(attachmentResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAttachmentsById action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // POST: /attachments/create
        [HttpPost]
        public async Task<IActionResult> CreateAttachment([FromBody] AttachmentForCreationDto attachment)
        {
            try
            {
                if (attachment == null)
                {
                    _logger.LogError("Attachments object sent from client is null.");
                    return BadRequest("Attachments object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid attachment object sent from client.");
                    return BadRequest("Invalid model object");
                }

                await _attachmentService.CreateAttachment(attachment);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateAttachments action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // PUT: /attachments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttachment(int id, [FromBody] AttachmentForCreationDto attachment)
        {
            try
            {
                if (attachment == null)
                {
                    _logger.LogError("Attachments object sent from client is null.");
                    return BadRequest("Attachments object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid attachment object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var attachmentEntity = _attachmentService.GetAttachmentById(id);
                if (attachmentEntity == null)
                {
                    _logger.LogError($"Attachments with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                await _attachmentService.UpdateAttachment(id, attachment);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateAttachments action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // DELETE: /attachments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttachment(int id)
        {
            try
            {
                var attachment = _attachmentService.GetAttachmentById(id);
                if (attachment == null)
                {
                    _logger.LogError($"Attachment with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                await _attachmentService.DeleteAttachment(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteAttachment action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // TODO
        // POST: /attachments/upload/{issueId}
        [HttpPut("[action]/{issueId}")]
        public async Task<IActionResult> Upload(int issueId, IFormFile file = null)
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
    }
}
