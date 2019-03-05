using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ics_iw5_ProjDatabases.DAL.Entity
{
    [Table("Comments")]
    class Comment : BaseEntity
    {
        [Key]
        [Column]
        public int CommentID { get; set; }        
    }
}
