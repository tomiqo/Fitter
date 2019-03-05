using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ics_iw5_ProjDatabases.DAL.Enums
{
    public enum Status{
        Online,
        Offline,
        //Nepřítomen,
        //Nerušit
    }
}
