using System;
using Fitter.DAL.Entity.Base;
using Fitter.DAL.Enums;

namespace Fitter.DAL.Entity
{
    public class Attachment : BaseEntity
    {
        public string Name { get; set; }
        public FileType FileType { get; set; } 
        public byte[] File { get; set; }
        public Guid CurrentPostId { get; set; }
        public Post Post { get; set; }
    }
}
