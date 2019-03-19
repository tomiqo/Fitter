using System.ComponentModel.DataAnnotations.Schema;
using Project.DAL.Entity.Base;

namespace Project.DAL.Entity
{
    [Table("Tag")]
    public class Tag : BaseEntity
    {
        [Column]
        [ForeignKey("UserID")]
        public int UserID { get; set; }
    }
}
