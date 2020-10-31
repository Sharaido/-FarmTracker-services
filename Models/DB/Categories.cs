using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class Categories
    {
        public Categories()
        {
            CategoryOfProperties = new HashSet<CategoryOfProperties>();
            EntityOfFp = new HashSet<EntityOfFp>();
        }

        public int Cuid { get; set; }
        public string Name { get; set; }
        public bool? SubCategoryFlag { get; set; }
        public int? SubCategoryOfCuid { get; set; }

        public virtual ICollection<CategoryOfProperties> CategoryOfProperties { get; set; }
        public virtual ICollection<EntityOfFp> EntityOfFp { get; set; }
    }
}
