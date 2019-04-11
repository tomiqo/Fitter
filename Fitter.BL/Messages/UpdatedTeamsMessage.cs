﻿using System;
using System.Collections.Generic;
using System.Text;
using Fitter.BL.Model;

namespace Fitter.BL.Messages
{
    public class UpdatedTeamsMessage : IMessage
    {
        public TeamDetailModel Model { get; set; }

        public UpdatedTeamsMessage(TeamDetailModel model)
        {
            Model = model;
        }
    }
}
