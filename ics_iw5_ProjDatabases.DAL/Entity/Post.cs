using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ics_iw5_ProjDatabases.DAL.Enums
{
    // 2.
    [Table("Post")]
    //-----
    class Post : BaseEntity
    {
        [Key]
        [Column]
        public int PostID { get; set; }
        [Column]
        public string Title { get; set; }
        /* 2. možnost zapisu do tabulky
        [Column]
        public int TableCommpetPostID { get; set; }
        */
        // 2. 
        [Column]
        [ForeignKey("TeamID")]
        public int TableMyTeam { get; set; }
        //-----
    }
}
