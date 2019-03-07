using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using ics_iw5_ProjDatabases.DAL.Entity.Base;

namespace ics_iw5_ProjDatabases.DAL.Entity
{
    [Table("Attachment")]
    public class Attachment : BaseEntity
    {
        [Column]
        public byte[] File { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public DateTime Time { get; set; }
    }
}
