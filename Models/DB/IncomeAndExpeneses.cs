using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class IncomeAndExpeneses
    {
        public Guid Ieuid { get; set; }
        public Guid Fuid { get; set; }
        public bool IncomeFlag { get; set; }
        public DateTime? Date { get; set; }
        public string Head { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedByUuid { get; set; }
        public bool DeletedFlag { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedByUuid { get; set; }

        public virtual Users DeletedByUu { get; set; }
        public virtual Farms Fu { get; set; }
    }
}
