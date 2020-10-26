using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class CodeType
    {
        public CodeType()
        {
            GeneratedUcodes = new HashSet<GeneratedUcodes>();
        }

        public int Ctuid { get; set; }
        public string Type { get; set; }

        public virtual ICollection<GeneratedUcodes> GeneratedUcodes { get; set; }
    }
}
