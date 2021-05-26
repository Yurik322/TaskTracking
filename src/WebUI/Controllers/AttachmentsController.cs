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
    [Route("api/attachment")]
    [Authorize]
    [ApiController]
    public class AttachmentsController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        //TODO
        private readonly IUnitOfWork _repository;


        public AttachmentsController(IAttachmentService attachmentService, ILoggerManager logger, IMapper mapper, IUnitOfWork repository)
        {
            _attachmentService = attachmentService;
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
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

                return CreatedAtRoute("AttachmentById", new { id = createdAttachment.Id }, createdAttachment);
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
