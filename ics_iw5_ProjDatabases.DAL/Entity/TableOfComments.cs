using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ics_iw5_ProjDatabases.DAL.Entity
{
    class TableOfComments : MainPostEntity
    {
        [Key]
        [Column]
        public int CommentID { get; set; }    
    }
}
