﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ics_iw5_ProjDatabases.DAL.Interface
{
    public interface IEntity
    {
        Guid ID { get; }
    }
}