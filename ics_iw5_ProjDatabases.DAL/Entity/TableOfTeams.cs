using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ics_iw5_ProjDatabases.DAL.Entity
{
    class TableOfTeams
    {
        [Key]
        [Column]
        [ForeignKey("TeamID")]
        public int Team { get; set; }
    }
}
