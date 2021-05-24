using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class AttachmentRepository : RepositoryBase<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Attachment> GetAllAttachments(bool trackChanges) =>
            FindAll(trackChanges).ToList();

        public Attachment GetAttachmentById(int attachmentId)
        {
            return FindByCondition(x => x.Id.Equals(attachmentId))
                .FirstOrDefault();
        }

        public Attachment GetAttachmentWithDetails(int attachmentId)
        {
            return FindByCondition(owner => owner.Id.Equals(attachmentId)).FirstOrDefault();
        }

        public void CreateAttachment(Attachment attachment)
        {
            Create(attachment);
        }

        //TODO
        public void UpdateAttachment(Attachment attachment)
        {
            Update(attachment);
        }

        public void DeleteAttachment(Attachment attachment)
        {
            throw new NotImplementedException();
        }
    }
}
