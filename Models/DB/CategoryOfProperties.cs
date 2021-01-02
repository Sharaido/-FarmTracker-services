using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class CategoryOfProperties
    {
        public CategoryOfProperties()
        {
            AddCopvalues = new HashSet<AddCopvalues>();
            Copvalues = new HashSet<Copvalues>();
            EntityCopvalues = new HashSet<EntityCopvalues>();
        }

        public int Puid { get; set; }
        public string Name { get; set; }
        public int Tuid { get; set; }
        public int Cuid { get; set; }

        public virtual Categories Cu { get; set; }
        public virtual Coptypes Tu { get; set; }
        public virtual ICollection<AddCopvalues> AddCopvalues { get; set; }
        public virtual ICollection<Copvalues> Copvalues { get; set; }
        public virtual ICollection<EntityCopvalues> EntityCopvalues { get; set; }
    }
}
