using System;
using System.ComponentModel.DataAnnotations.Schema;
using Project.DAL.Entity.Base;
using Project.DAL.Enums;

namespace Project.DAL.Entity
{
    public class Attachment : BaseEntity
    {
        public string Name { get; set; }
        public int FileSize  { get; set; }
        public FileType FileType { get; set; } 
        public byte[] File { get; set; }
        public Guid CurrentPostId { get; set; }
        public Post Post { get; set; }
    }
}
