using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class FarmProperties
    {
        public FarmProperties()
        {
            EntityOfFp = new HashSet<EntityOfFp>();
        }

        public Guid Puid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Tuid { get; set; }
        public Guid Fuid { get; set; }
        public Guid CreatedByUuid { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool DeletedFlag { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedByUuid { get; set; }

        public virtual Users CreatedByUu { get; set; }
        public virtual Users DeletedByUu { get; set; }
        public virtual Farms Fu { get; set; }
        public virtual FarmPropertyType Tu { get; set; }
        public virtual ICollection<EntityOfFp> EntityOfFp { get; set; }
    }
}
