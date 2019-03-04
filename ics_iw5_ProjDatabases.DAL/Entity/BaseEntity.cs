using System;
using System.Collections.Generic;
using System.Text;

namespace ics_iw5_ProjDatabases.DAL
{
    class BaseEntity : Interface.IEntity
    {
        public Guid ID { get; set; }
    }
}
