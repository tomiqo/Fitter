using System.ComponentModel.DataAnnotations.Schema;
using ics_iw5_ProjDatabases.DAL.Entity.Base;

namespace ics_iw5_ProjDatabases.DAL.Entity
{
    [Table("Tag")]
    public class Tag : BaseEntity
    {
        [ForeignKey("UserID")]
        [Column]
        public int UserID { get; set; }
    }
}
