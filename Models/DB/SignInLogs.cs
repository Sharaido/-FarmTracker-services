using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class SignInLogs
    {
        public Guid Luid { get; set; }
        public string IpAdd { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string Hostname { get; set; }
        public DateTime? Date { get; set; }
        public string AttemptedPassword { get; set; }
        public bool AttemptedResult { get; set; }
        public Guid Uuid { get; set; }

        public virtual Users Uu { get; set; }
    }
}
