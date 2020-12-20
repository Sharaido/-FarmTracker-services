using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class EntityOfFp
    {
        public EntityOfFp()
        {
            EntityCopvalues = new HashSet<EntityCopvalues>();
            EntityDetails = new HashSet<EntityDetails>();
        }

        public Guid Euid { get; set; }
        public int Cuid { get; set; }
        public Guid Puid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Count { get; set; }
        public DateTime? PurchasedDate { get; set; }
        public decimal? Cost { get; set; }
        public bool SoldFlag { get; set; }
        public DateTime? SoldDate { get; set; }
        public decimal? SoldPrice { get; set; }
        public string SoldDetail { get; set; }
        public Guid? SoldByUuid { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid CreatedByUuid { get; set; }
        public bool DeletedFlag { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedByUuid { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public virtual Users CreatedByUu { get; set; }
        public virtual Categories Cu { get; set; }
        public virtual Users DeletedByUu { get; set; }
        public virtual FarmProperties Pu { get; set; }
        public virtual Users SoldByUu { get; set; }
        public virtual ICollection<EntityCopvalues> EntityCopvalues { get; set; }
        public virtual ICollection<EntityDetails> EntityDetails { get; set; }
    }
}
