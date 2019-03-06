using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ics_iw5_ProjDatabases.DAL.Entity
{
    class TableOfUsers
    {
        [Key]
        [Column]
        [ForeignKey("UserID")]
        public int User { get; set; }
    }
}
