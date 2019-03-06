using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ics_iw5_ProjDatabases.DAL.Enums
{
    [Table("Post")]
    class Post : BaseEntity
    {
        [Key]
        [Column]
        public int PostID { get; set; }
        [Column]
        public string Title { get; set; }
        /* Druhá možnost zapisu do tabulky
        [Column]
        public int TablePostID { get; set; }
        */
    }
}
