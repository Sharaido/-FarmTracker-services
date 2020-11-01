using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class Collaborators
    {
        public Guid Fuid { get; set; }
        public Guid Uuid { get; set; }
        public Guid Ruid { get; set; }

        public virtual Farms Fu { get; set; }
        public virtual CRoles Ru { get; set; }
        public virtual Users Uu { get; set; }
    }
}
