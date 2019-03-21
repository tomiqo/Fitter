using System;
using System.ComponentModel.DataAnnotations.Schema;
using Project.DAL.Entity.Base;
using Project.DAL.Enums;

namespace Project.DAL.Entity
{
    [Table("Attachment")]
    public class Attachment : BaseEntity
    {
        [Column]
        public string Name { get; set; }
        [Column]
        public int FileSize  { get; set; }
        [Column]
        public FileType FileType { get; set; } 
        [Column]
        public byte[] File { get; set; }
        [Column]
        public Comment Comment { get; set; } //
    }
}
