using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class Adds
    {
        public Guid Auid { get; set; }
        public int? Cuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? Discount { get; set; }
        public Guid? CreatedByUuid { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool ConfirmedFlag { get; set; }
        public Guid? ConfirmedByUuid { get; set; }
        public bool PublishedFlag { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool DeletedFlag { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedByUuid { get; set; }

        public virtual Users ConfirmedByUu { get; set; }
        public virtual Users CreatedByUu { get; set; }
        public virtual Categories Cu { get; set; }
        public virtual Users DeletedByUu { get; set; }
    }
}
