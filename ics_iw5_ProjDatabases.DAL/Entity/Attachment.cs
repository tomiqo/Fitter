using System;
using System.ComponentModel.DataAnnotations.Schema;
using ics_iw5_ProjDatabases.DAL.Entity.Base;

namespace ics_iw5_ProjDatabases.DAL.Entity
{
    [Table("Attachment")]
    public class Attachment : BaseEntity
    {
        [Column]
        public string Name { get; set; }
        [Column]
        public int FileSize  { get; set; } // KB
        [Column]
        public string Type { get; set; } // picture, video, file (.zip , .rar ......)
        [Column]
        public byte[] File { get; set; }
        [Column]
        public DateTime Time { get; set; }
    }
}
