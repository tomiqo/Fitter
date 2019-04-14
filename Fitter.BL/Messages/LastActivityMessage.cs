using System;
using System.Collections.Generic;
using System.Text;

namespace Fitter.BL.Messages
{
    public class LastActivityMessage : IMessage
    {
        public string LastPost { get; set; }
        public string LastComment { get; set; }
    }
}
