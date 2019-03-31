using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.DAL.Entity.Base;

namespace Project.DAL.Entity
{
    [Table("Comment")]
    public class Comment : BaseEntity
    {
        [Column]
        [ForeignKey("UserID")]
        public User Author { get; set; }
        [Column]
        public string Text { get; set; }
        [Column]
        public DateTime Created { get; set; }
        [Column]
        public ICollection<Attachment> Attachments { get; set; }
        [Column]
        public ICollection<Comment> Comments { get; set; }
        [Column]
        public ICollection<User> Tags { get; set; }
        [Column]
        public Comment Parent { get; set; } 
        public Post Post { get; set; }

    }
}
