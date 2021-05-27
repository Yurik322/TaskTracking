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
    [Route("api/attachment")]
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

        // GET: /attachment/companies
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
        [HttpGet("{id}", Name = "AttachmentById")]
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

        //TODO
        // GET: /attachment/5/employee
        [HttpGet("{id}/employee")]
        public IActionResult GetAttachmentWithDetails(int id)
        {
            try
            {
                var attachment = _repository.Attachment.GetAttachmentWithDetails(id);
                if (attachment == null)
                {
                    _logger.LogError($"Attachment with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned attachment with details for id: {id}");

                    var attachmentResult = _mapper.Map<AttachmentDto>(attachment);
                    return Ok(attachmentResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAttachmentWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: /attachment/create
        [HttpPost]
        public async Task<IActionResult> CreateAttachment([FromBody] AttachmentForCreationDto attachment)
        {
            try
            {
                if (attachment == null)
                {
                    _logger.LogError("Attachment object sent from client is null.");
                    return BadRequest("Attachment object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid attachment object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var attachmentEntity = _mapper.Map<Attachment>(attachment);

                _repository.Attachment.CreateAttachment(attachmentEntity);
                await _repository.SaveAsync();

                var createdAttachment = _mapper.Map<AttachmentDto>(attachmentEntity);

                return Ok();
                //return CreatedAtRoute("AttachmentById", new { id = createdAttachment.Id }, createdAttachment);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateAttachment action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        // PUT: /attachment/5/edit
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttachment(int id, [FromBody] AttachmentForCreationDto attachment)
        {
            try
            {
                if (attachment == null)
                {
                    _logger.LogError("Attachment object sent from client is null.");
                    return BadRequest("Attachment object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid attachment object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var attachmentEntity = _repository.Attachment.GetAttachmentById(id);
                if (attachmentEntity == null)
                {
                    _logger.LogError($"Attachment with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(attachment, attachmentEntity);

                _repository.Attachment.UpdateAttachment(attachmentEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateAttachment action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: /attachment/5
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
        // POST: /Attachments/Upload/{issueId}
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
                Issue = issue
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
