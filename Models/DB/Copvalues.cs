using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class Copvalues
    {
        public string Value { get; set; }
        public int Puid { get; set; }

        public virtual CategoryOfProperties Pu { get; set; }
    }
}
