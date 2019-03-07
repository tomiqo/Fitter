using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ics_iw5_ProjDatabases.DAL.Entity.Base;

namespace ics_iw5_ProjDatabases.DAL.Entity
{
    public class Comment : BaseEntity
    {
        [Column]
        [ForeignKey("UserID")]
        public int Author { get; set; }
        [Column]
        public string Text { get; set; }
        [Column]
        public DateTime Time { get; set; }
        [Column]
        public ICollection<Tag> Tags { get; set; }
        [Column]
        public ICollection<Attachment> Attachments { get; set; }
    }
}
