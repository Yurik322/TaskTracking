using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BLL.EtitiesDTO;
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

        public IEnumerable<AttachmentDto> GetAllAttachments()
        {
            var attachments = _mapper.Map<IEnumerable<Attachment>, IEnumerable<AttachmentDto>>(_repository.Attachment.GetAllAttachments(trackChanges: false));

            return _mapper.Map<IEnumerable<AttachmentDto>>(attachments);
        }

        public void AddAsync(AttachmentDto model)
        {
            var newModel = _mapper.Map<AttachmentDto, Attachment>(model);
            _repository.Attachment.Create(newModel);
            _repository.SaveAsync();
        }
    }
}
