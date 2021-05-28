using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IAttachmentRepository : IRepositoryBase<Attachment>
    {
        Task<IEnumerable<Attachment>> GetAllAttachments(bool trackChanges);
        Task<Attachment> GetAttachmentById(int attachmentId);
        Attachment GetAttachmentWithDetails(int attachmentId);
        void CreateAttachment(Attachment attachment);
        void UpdateAttachment(Attachment attachment);
        void DeleteAttachment(Attachment attachment);
        Task<IEnumerable<Attachment>> WhereIsAttachment(int attachmentId);
    }
}
