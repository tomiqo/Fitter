using System;
using System.Collections.Generic;
using System.Text;

namespace ics_iw5_ProjDatabases.DAL.Entity
{
    class Comment : BaseEntity
    {
        public Guid Author { get; set; }  
        public string Text { get; set; }
        public string Time { get; set; }
    }
}
