using System;
using System.Collections.Generic;

namespace FarmTracker_services.Models.DB
{
    public partial class Users
    {
        public Users()
        {
            AddsConfirmedByUu = new HashSet<Adds>();
            AddsCreatedByUu = new HashSet<Adds>();
            AddsDeletedByUu = new HashSet<Adds>();
            CRolesCreatedByUu = new HashSet<CRoles>();
            CRolesDeletedByUu = new HashSet<CRoles>();
            Collaborators = new HashSet<Collaborators>();
            EntityDetailsCreatedByUu = new HashSet<EntityDetails>();
            EntityDetailsDeletedByUu = new HashSet<EntityDetails>();
            EntityOfFpCreatedByUu = new HashSet<EntityOfFp>();
            EntityOfFpDeletedByUu = new HashSet<EntityOfFp>();
            EntityOfFpSoldByUu = new HashSet<EntityOfFp>();
            FarmEntities = new HashSet<FarmEntities>();
            FarmPropertiesCreatedByUu = new HashSet<FarmProperties>();
            FarmPropertiesDeletedByUu = new HashSet<FarmProperties>();
            FarmsCreatedByUu = new HashSet<Farms>();
            FarmsDeletedByUu = new HashSet<Farms>();
            GeneratedUcodes = new HashSet<GeneratedUcodes>();
            IncomeAndExpeneses = new HashSet<IncomeAndExpeneses>();
            InverseDeletedByUu = new HashSet<Users>();
            Roles = new HashSet<Roles>();
            Sessions = new HashSet<Sessions>();
            SignInLogs = new HashSet<SignInLogs>();
        }

        public Guid Uuid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool? EmailActivated { get; set; }
        public string PhoneNumber { get; set; }
        public bool? PhoneNumberActivated { get; set; }
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
        public virtual ICollection<Adds> AddsConfirmedByUu { get; set; }
        public virtual ICollection<Adds> AddsCreatedByUu { get; set; }
        public virtual ICollection<Adds> AddsDeletedByUu { get; set; }
        public virtual ICollection<CRoles> CRolesCreatedByUu { get; set; }
        public virtual ICollection<CRoles> CRolesDeletedByUu { get; set; }
        public virtual ICollection<Collaborators> Collaborators { get; set; }
        public virtual ICollection<EntityDetails> EntityDetailsCreatedByUu { get; set; }
        public virtual ICollection<EntityDetails> EntityDetailsDeletedByUu { get; set; }
        public virtual ICollection<EntityOfFp> EntityOfFpCreatedByUu { get; set; }
        public virtual ICollection<EntityOfFp> EntityOfFpDeletedByUu { get; set; }
        public virtual ICollection<EntityOfFp> EntityOfFpSoldByUu { get; set; }
        public virtual ICollection<FarmEntities> FarmEntities { get; set; }
        public virtual ICollection<FarmProperties> FarmPropertiesCreatedByUu { get; set; }
        public virtual ICollection<FarmProperties> FarmPropertiesDeletedByUu { get; set; }
        public virtual ICollection<Farms> FarmsCreatedByUu { get; set; }
        public virtual ICollection<Farms> FarmsDeletedByUu { get; set; }
        public virtual ICollection<GeneratedUcodes> GeneratedUcodes { get; set; }
        public virtual ICollection<IncomeAndExpeneses> IncomeAndExpeneses { get; set; }
        public virtual ICollection<Users> InverseDeletedByUu { get; set; }
        public virtual ICollection<Roles> Roles { get; set; }
        public virtual ICollection<Sessions> Sessions { get; set; }
        public virtual ICollection<SignInLogs> SignInLogs { get; set; }
    }
}
