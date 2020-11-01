using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class Roles
    {
        public Roles()
        {
            Users = new HashSet<Users>();
        }

        public int Ruid { get; set; }
        public Guid? CreatedByUuid { get; set; }
        public string Name { get; set; }
        public bool SysRoleFlag { get; set; }
        public bool CanEnterAdminDashboard { get; set; }

        public virtual Users CreatedByUu { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
