using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ics_iw5_ProjDatabases.DAL.Entity
{
    // 1 ...
    [Table("Comments")] 
    // 2. odstranění jedné tabulky a generování nových
    class Comment : BaseEntity
    {
        [Key]
        [Column]
        public int CommentID { get; set; }    
        // 1. možnost
        [Column]
        [ForeignKey("PostID")]
        public int MyPost { get; set; }
    }
}
