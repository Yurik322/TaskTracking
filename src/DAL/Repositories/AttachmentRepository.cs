using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class AttachmentRepository : RepositoryBase<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Attachment>> GetAllAttachments(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToListAsync();

        public async Task<Attachment> GetAttachmentById(int attachmentId)
        {
            return await FindByCondition(x => x.AttachmentId.Equals(attachmentId))
                .FirstOrDefaultAsync();
        }

        public Attachment GetAttachmentWithDetails(int attachmentId)
        {
            return FindByCondition(owner => owner.AttachmentId.Equals(attachmentId)).FirstOrDefault();
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

        public async Task<IEnumerable<Attachment>> WhereIsAttachment(int attachmentId)
        {
            return await FindByCondition(i => i.Issue.IssueId == attachmentId)
                .OrderByDescending(a => a.AttachmentId).ToListAsync();
        }
    }
}
