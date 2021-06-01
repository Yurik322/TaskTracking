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
    /// <summary>
    /// Class repository for work with attachments.
    /// </summary>
    public class AttachmentRepository : RepositoryBase<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        /// <summary>
        /// Method for get all attachments from db.
        /// </summary>
        /// <param name="trackChanges">parameter that help in tracking changes.</param>
        /// <returns>list of all articles.</returns>
        public async Task<IEnumerable<Attachment>> GetAllAttachments(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToListAsync();

        /// <summary>
        /// Method for get attachment by id from db.
        /// </summary>
        /// <param name="attachmentId">id of attachment.</param>
        /// <returns>get attachment.</returns>
        public async Task<Attachment> GetAttachmentById(int attachmentId)
        {
            return await FindByCondition(x => x.AttachmentId.Equals(attachmentId))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method for get attachment with details from db.
        /// </summary>
        /// <param name="attachmentId">id of attachment.</param>
        /// <returns>get attachment with details.</returns>
        public Attachment GetAttachmentWithDetails(int attachmentId)
        {
            return FindByCondition(owner => owner.AttachmentId.Equals(attachmentId)).FirstOrDefault();
        }

        /// <summary>
        /// Method for create attachment in db.
        /// </summary>
        /// <param name="attachment">new attachment.</param>
        public void CreateAttachment(Attachment attachment)
        {
            Create(attachment);
        }

        /// <summary>
        /// Method for update attachment in db.
        /// </summary>
        /// <param name="attachment">updated attachment.</param>
        public void UpdateAttachment(Attachment attachment)
        {
            Update(attachment);
        }

        /// <summary>
        /// Method for deleting attachment from db.
        /// </summary>
        /// <param name="attachment">deleted attachment.</param>
        public void DeleteAttachment(Attachment attachment)
        {
            Delete(attachment);
        }

        /// <summary>
        /// Method for searching attachments in db.
        /// </summary>
        /// <param name="attachmentId">search attachment.</param>
        /// <returns>list of attachments.</returns>
        public async Task<IEnumerable<Attachment>> WhereIsAttachment(int attachmentId)
        {
            return await FindByCondition(i => i.Issue.IssueId == attachmentId)
                .OrderByDescending(a => a.AttachmentId).ToListAsync();
        }
    }
}
