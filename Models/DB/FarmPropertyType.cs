using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class FarmPropertyType
    {
        public FarmPropertyType()
        {
            FarmProperties = new HashSet<FarmProperties>();
        }

        public int Tuid { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FarmProperties> FarmProperties { get; set; }
    }
}
