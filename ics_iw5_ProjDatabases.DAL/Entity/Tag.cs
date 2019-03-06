using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ics_iw5_ProjDatabases.DAL.Entity
{
    [Table("Tag")]
    class Tag
    {
        [Key]
        [Column]
        public int TagID { get; set; }

        [Column]
        [ForeignKey("PostID")]
        public int? PostFID { get; set; }

        [Column]
        [ForeignKey("CommentID")]
        public int? CommentsFID { get; set; }
    }
}
