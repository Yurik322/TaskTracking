using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EtitiesDTO;
using BLL.EtitiesDTO.Attachment;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public AttachmentService(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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

        public async Task CreateAttachment(AttachmentForCreationDto project)
        {
            var projectEntity = _mapper.Map<Attachment>(project);

            _repository.Attachment.CreateAttachment(projectEntity);
            await _repository.SaveAsync();
        }

        public async Task UpdateAttachment(int id, AttachmentForCreationDto project)
        {
            var projectEntity = await _repository.Attachment.GetAttachmentById(id);

            _mapper.Map(project, projectEntity);
            _repository.Attachment.UpdateAttachment(projectEntity);
            await _repository.SaveAsync();
        }

        public async Task DeleteAttachment(int id)
        {
            var projectEntity = await _repository.Attachment.GetAttachmentById(id);

            _repository.Attachment.DeleteAttachment(projectEntity);
            await _repository.SaveAsync();
        }
    }
}
