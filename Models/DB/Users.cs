using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class Users
    {
        public Users()
        {
            GeneratedUcodes = new HashSet<GeneratedUcodes>();
            InverseDeletedByUu = new HashSet<Users>();
            Sessions = new HashSet<Sessions>();
            SignInLogs = new HashSet<SignInLogs>();
        }

        public Guid Uuid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool EmailActivated { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberActivated { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ProfilPic { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool DeletedFlag { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedByUuid { get; set; }
        public int Ruid { get; set; }
        public int Mtuid { get; set; }

        public virtual Users DeletedByUu { get; set; }
        public virtual MemberType Mtu { get; set; }
        public virtual Roles Ru { get; set; }
        public virtual ICollection<GeneratedUcodes> GeneratedUcodes { get; set; }
        public virtual ICollection<Users> InverseDeletedByUu { get; set; }
        public virtual ICollection<Sessions> Sessions { get; set; }
        public virtual ICollection<SignInLogs> SignInLogs { get; set; }
    }
}
