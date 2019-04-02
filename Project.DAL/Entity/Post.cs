using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.DAL.Entity.Base;

namespace Project.DAL.Entity
{
    public class Post : BasePost
    {
        public string Title { get; set; }
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public Guid CurrentTeamId { get; set; }
        public Team Team { get; set; }
    }
}
