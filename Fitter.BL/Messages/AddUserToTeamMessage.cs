﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Fitter.BL.Messages
{
    public class AddUserToTeamMessage : IMessage
    {
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
    }
}
