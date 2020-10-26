using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class GeneratedUcodes
    {
        public Guid Guc { get; set; }
        public Guid? ForUuid { get; set; }
        public string ForIp { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool? IsValid { get; set; }
        public int? Ctuid { get; set; }

        public virtual CodeType Ctu { get; set; }
        public virtual Users ForUu { get; set; }
    }
}
