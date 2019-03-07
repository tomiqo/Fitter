using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ics_iw5_ProjDatabases.DAL.Entity
{
    [Table("Post")]
    public class Post : Comment
    {
        [Column]
        public string Title { get; set; }
        [Column]
        public ICollection<Comment> Comments { get; set; }
    }
}
