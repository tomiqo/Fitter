using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DAL.Entity
{
    [Table("Post")]
    public class Post : Comment
    {
        [Column]
        public string Title { get; set; }
    }
}
