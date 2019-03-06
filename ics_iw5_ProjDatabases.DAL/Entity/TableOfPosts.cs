using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ics_iw5_ProjDatabases.DAL.Enums
{
    class TableOfPosts : MainPostEntity
    {
        [Key]
        [Column]
        public int PostID { get; set; }
        [Column]
        public string Title { get; set; }
        [Column]
        //Pointer to table of comments
        public int TableOfComments { get; set; }
    }
}
