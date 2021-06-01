using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EtitiesDTO;
using BLL.EtitiesDTO.Attachment;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Entities.Enums;
using DAL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BLL.Services
{
    /// <summary>
    /// Class for attachment services.
    /// </summary>
    public class AttachmentService : IAttachmentService
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AttachmentService(IUnitOfWork repository, IMapper mapper, IWebHostEnvironment env)
        {
            _repository = repository;
            _mapper = mapper;
            _env = env;
        }

        /// <summary>
        /// Method for get all AttachmentDto objects.
        /// </summary>
        /// <returns>collection of AttachmentDto.</returns>
        public async Task<IEnumerable<AttachmentDto>> GetAllAttachments()
        {
            var attachments =
                _mapper.Map<IEnumerable<Attachment>, IEnumerable<AttachmentDto>>(await _repository.Attachment.GetAllAttachments(trackChanges: false));

            return _mapper.Map<IEnumerable<AttachmentDto>>(attachments);
        }

        /// <summary>
        /// Method for get AttachmentDto object by id.
        /// </summary>
        /// <param name="id">id of AttachmentDto.</param>
        /// <returns>object of AttachmentDto</returns>
        public async Task<AttachmentDto> GetAttachmentById(int id)
        {
            var project = await _repository.Attachment.GetAttachmentById(id);

            return _mapper.Map<AttachmentDto>(project);
        }

        /// <summary>
        /// Method for create AttachmentForCreationDto.
        /// </summary>
        /// <param name="attachment">new attachment.</param>
        /// <returns>new object.</returns>
        public async Task CreateAttachment(AttachmentForCreationDto attachment)
        {
            var projectEntity = _mapper.Map<Attachment>(attachment);

            _repository.Attachment.CreateAttachment(projectEntity);
            await _repository.SaveAsync();
        }

        /// <summary>
        /// Method for update AttachmentForCreationDto.
        /// </summary>
        /// <param name="id">id of updated attachment.</param>
        /// <param name="attachment">updated attachment.</param>
        /// <returns>updated object.</returns>
        public async Task UpdateAttachment(int id, AttachmentForCreationDto attachment)
        {
            var projectEntity = await _repository.Attachment.GetAttachmentById(id);

            _mapper.Map(attachment, projectEntity);
            _repository.Attachment.UpdateAttachment(projectEntity);
            await _repository.SaveAsync();
        }

        /// <summary>
        /// Method for deleting AttachmentDto.
        /// </summary>
        /// <param name="id">id of attachment.</param>
        /// <returns>deleted object.</returns>
        public async Task DeleteAttachment(int id)
        {
            var projectEntity = await _repository.Attachment.GetAttachmentById(id);

            _repository.Attachment.DeleteAttachment(projectEntity);
            await _repository.SaveAsync();
        }

        /// <summary>
        /// Method for creating new file for attachment.
        /// </summary>
        /// <param name="issueId">id of issue.</param>
        /// <param name="file">object of file.</param>
        /// <returns>new file object.</returns>
        public async Task CreateFile(int issueId, IFormFile file)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" +
                           DateTime.Now.ToString("yyyyMMddHHmmss") +
                           Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_env.WebRootPath, fileName);
            await using (var fs = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }
            var attachment = new AttachmentForCreationDto()
            {
                Name = Path.GetFileNameWithoutExtension(file.FileName),
                CreatedAt = DateTime.Now,
                Path = fileName,
                FileType = GetFileType(file),
                IssueId = issueId
            };

            var attachmentEntity = _mapper.Map<Attachment>(attachment);
            _repository.Attachment.CreateAttachment(attachmentEntity);
            await _repository.SaveAsync();
        }

        /// <summary>
        /// Method for getting type of file.
        /// </summary>
        /// <param name="file">object of file.</param>
        /// <returns>type of file.</returns>
        public FileType GetFileType(IFormFile file)
        {
            string[] allowedExtensions = { "png", "jpg", "jpeg" };
            var fileExtension = Path.GetExtension(file.FileName);
            if (allowedExtensions.Any(e => fileExtension != null && fileExtension.Contains(e)))
                return FileType.Image;

            return FileType.Document;
        }
    }
}
