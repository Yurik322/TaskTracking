using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities.Enums;

namespace BLL.EtitiesDTO
{
    public class AttachmentForCreationDto
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public FileType FileType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
