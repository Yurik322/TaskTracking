using System;
using System.Collections.Generic;
using System.Text;
using BLL.EtitiesDTO;
using BLL.EtitiesDTO.Attachment;

namespace BLL.Interfaces
{
    public interface IAttachmentService
    {
        IEnumerable<AttachmentDto> GetAllAttachments();
        void AddAsync(AttachmentDto model);


        //IEnumerable<AttachmentDto> FindBooks(string searchName);
        //AttachmentDto GetBook(int? id);
        //IEnumerable<AttachmentDto> GetBooks(string category, string author);
        //IEnumerable<AttachmentDto> GetBooks();
        //void DeleteBook(int id);
        //void Update(AttachmentDto bookDTO);
        //void CreateBook(AttachmentDto bookDTO);
    }
}
