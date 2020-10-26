using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class MemberType
    {
        public MemberType()
        {
            Users = new HashSet<Users>();
        }

        public int Mtuid { get; set; }
        public string Name { get; set; }
        public int? FarmLimit { get; set; }
        public int? PropertyLimit { get; set; }
        public int? CollaboratorLimit { get; set; }
        public int? AdLimit { get; set; }
        public bool? SupportFlag { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
