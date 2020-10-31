using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class EntityCopvalues
    {
        public Guid Euid { get; set; }
        public int Puid { get; set; }
        public string Value { get; set; }

        public virtual EntityOfFp Eu { get; set; }
        public virtual CategoryOfProperties Pu { get; set; }
    }
}
