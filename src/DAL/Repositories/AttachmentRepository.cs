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
            FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToList();

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

        public void UpdateAttachment(Attachment attachment)
        {
            Update(attachment);
        }

        public void DeleteAttachment(Attachment attachment)
        {
            Delete(attachment);
        }

        public IEnumerable<Attachment> WhereIsAttachment(int attachmentId)
        {
            return FindByCondition(i => i.Issue.Id == attachmentId)
                .OrderByDescending(a => a.Id).ToList();

            //return _context.Attachments.Where(i => i.Issue.IssueId == id)
            //    .OrderByDescending(a => a.AttachmentId)
            //    .ToList();
        }
    }
}
