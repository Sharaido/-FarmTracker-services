using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class AddCopvalues
    {
        public Guid Auid { get; set; }
        public int Puid { get; set; }
        public string Value { get; set; }

        public virtual Adds Au { get; set; }
        public virtual CategoryOfProperties Pu { get; set; }
    }
}
