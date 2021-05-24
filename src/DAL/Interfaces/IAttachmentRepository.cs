using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IAttachmentRepository : IRepositoryBase<Attachment>
    {
        IEnumerable<Attachment> GetAllAttachments(bool trackChanges);
        Attachment GetAttachmentById(int attachmentId);
        Attachment GetAttachmentWithDetails(int attachmentId);
        void CreateAttachment(Attachment attachment);
        void UpdateAttachment(Attachment attachment);
        void DeleteAttachment(Attachment attachment);
    }
}
