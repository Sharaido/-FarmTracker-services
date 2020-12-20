using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class Categories
    {
        public Categories()
        {
            Adds = new HashSet<Adds>();
            CategoryOfProperties = new HashSet<CategoryOfProperties>();
            EntityOfFp = new HashSet<EntityOfFp>();
            FarmEntities = new HashSet<FarmEntities>();
            FarmProperties = new HashSet<FarmProperties>();
        }

        public int Cuid { get; set; }
        public string Name { get; set; }
        public bool? EndPointFlag { get; set; }
        public int? SubCategoryOfCuid { get; set; }

        public virtual ICollection<Adds> Adds { get; set; }
        public virtual ICollection<CategoryOfProperties> CategoryOfProperties { get; set; }
        public virtual ICollection<EntityOfFp> EntityOfFp { get; set; }
        public virtual ICollection<FarmEntities> FarmEntities { get; set; }
        public virtual ICollection<FarmProperties> FarmProperties { get; set; }
    }
}
