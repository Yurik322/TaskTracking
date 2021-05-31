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

        public async Task<IEnumerable<AttachmentDto>> GetAllAttachments()
        {
            var attachments =
                _mapper.Map<IEnumerable<Attachment>, IEnumerable<AttachmentDto>>(await _repository.Attachment.GetAllAttachments(trackChanges: false));

            return _mapper.Map<IEnumerable<AttachmentDto>>(attachments);
        }

        public async Task<AttachmentDto> GetAttachmentById(int id)
        {
            var project = await _repository.Attachment.GetAttachmentById(id);

            return _mapper.Map<AttachmentDto>(project);
        }

        public async Task CreateAttachment(AttachmentForCreationDto attachment)
        {
            var projectEntity = _mapper.Map<Attachment>(attachment);

            _repository.Attachment.CreateAttachment(projectEntity);
            await _repository.SaveAsync();
        }

        public async Task UpdateAttachment(int id, AttachmentForCreationDto attachment)
        {
            var projectEntity = await _repository.Attachment.GetAttachmentById(id);

            _mapper.Map(attachment, projectEntity);
            _repository.Attachment.UpdateAttachment(projectEntity);
            await _repository.SaveAsync();
        }

        public async Task DeleteAttachment(int id)
        {
            var projectEntity = await _repository.Attachment.GetAttachmentById(id);

            _repository.Attachment.DeleteAttachment(projectEntity);
            await _repository.SaveAsync();
        }

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
