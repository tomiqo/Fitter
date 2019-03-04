using System;
using System.Collections.Generic;
using System.Text;

namespace ics_iw5_ProjDatabases.DAL.Entity
{
    class Attachment : BaseEntity
    {
        public object Picture { get; set; }
        public object Video { get; set; }
        public object File { get; set; }
    }
}
