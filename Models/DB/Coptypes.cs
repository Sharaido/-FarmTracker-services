using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class Coptypes
    {
        public Coptypes()
        {
            CategoryOfProperties = new HashSet<CategoryOfProperties>();
        }

        public int Tuid { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CategoryOfProperties> CategoryOfProperties { get; set; }
    }
}
