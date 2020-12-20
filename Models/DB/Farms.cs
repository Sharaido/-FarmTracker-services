using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class Farms
    {
        public Farms()
        {
            Collaborators = new HashSet<Collaborators>();
            FarmEntities = new HashSet<FarmEntities>();
            FarmProperties = new HashSet<FarmProperties>();
            IncomeAndExpeneses = new HashSet<IncomeAndExpeneses>();
        }

        public Guid Fuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public Guid CreatedByUuid { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool DeletedFlag { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedByUuid { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public virtual Users CreatedByUu { get; set; }
        public virtual Users DeletedByUu { get; set; }
        public virtual ICollection<Collaborators> Collaborators { get; set; }
        public virtual ICollection<FarmEntities> FarmEntities { get; set; }
        public virtual ICollection<FarmProperties> FarmProperties { get; set; }
        public virtual ICollection<IncomeAndExpeneses> IncomeAndExpeneses { get; set; }
    }
}
