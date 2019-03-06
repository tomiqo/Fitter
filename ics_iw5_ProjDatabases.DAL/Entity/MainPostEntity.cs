using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ics_iw5_ProjDatabases.DAL
{
    public abstract class MainPostEntity 
    {
        [Column]
        [ForeignKey("UserID")]
        public int Author { get; set; }
        [Column]
        public string Text { get; set; }
        [Column]
        public DateTime Time { get; set; }
        [Column]
        //Pointer to table of tags
        public int TableOfTags { get; set; }
        [Column]
        //Pointer to table of attachments
        public int TableOfAttachments { get; set; }
    }
}
