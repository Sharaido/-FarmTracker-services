using FarmTracker_services.Models.DB;
using FarmTracker_services.Models.Members;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmTracker_services.Data
{
    public class FarmTrackerRepo : IFarmTrackerRepo
    {
        private readonly farmTrackerContext _context;

        public FarmTrackerRepo(farmTrackerContext context)
        {
            _context = context;
        }

        [Obsolete]
        public IEnumerable<Farms> GetAllFarmsForUUID(Guid UUID)
        {
            return _context.Farms
                .FromSql($"SelectAllFarmsForUUID {UUID}")
                .ToList();
        }

        public EntityCopvalues GetCOPValue(Guid EUID, int PUID)
        {
            return _context.EntityCopvalues
                .Where(e => e.Euid == EUID && e.Puid == PUID)
                .FirstOrDefault();
        }

        public IEnumerable<EntityCopvalues> GetCOPValues(Guid EUID)
        {
            return _context.EntityCopvalues
                .Where(e => e.Euid == EUID);
        }

        public IEnumerable<EntityOfFp> GetEntitiesOfFP(Guid PUID)
        {
            return _context.EntityOfFp
                .Where(e => e.Puid == PUID && !e.DeletedFlag);
        }

        public EntityDetails GetEntityDetail(Guid EUID, Guid DUID)
        {
            return _context.EntityDetails
                .Where(e => e.Euid == EUID && e.Duid == DUID)
                .FirstOrDefault();
        }

        public IEnumerable<EntityDetails> GetEntityDetails(Guid EUID)
        {
            return _context.EntityDetails
                .Where(e => e.Euid == EUID && !e.DeletedFlag);
        }

        public EntityOfFp GetEntityOfFP(Guid PUID, Guid EUID)
        {
            return _context.EntityOfFp
                .Where(e => e.Puid == PUID && e.Euid == EUID && !e.DeletedFlag)
                .FirstOrDefault();
        }

        [Obsolete]
        public int GetFailedSignInRequestsInTheLast5MinFromUUID(Guid UUID)
        {
            return _context.SignInLogs
                .FromSql($"SelectFailedSignInRequestsInTheLast5MinFromUUID {UUID}")
                .ToList()
                .Count;
        }

        public Farms GetFarm(Guid FUID)
        {
            return _context.Farms.Where(e => e.Fuid == FUID && !e.DeletedFlag).FirstOrDefault();
        }

        public IEnumerable<FarmProperties> GetFarmProperties(Guid FUID)
        {
            return _context.FarmProperties.Where(e => e.Fuid == FUID && !e.DeletedFlag);
        }

        public FarmProperties GetFarmProperty(Guid FUID, Guid PUID)
        {
            return _context.FarmProperties.Where(e => e.Puid == PUID && e.Fuid == FUID && !e.DeletedFlag).FirstOrDefault();
        }

        [Obsolete]
        public GeneratedUcodes GetNewUCodeForSignUp(string ip)
        {
            return _context.GeneratedUcodes
                .FromSql($"InsertUCodeForSignUp {ip}")
                .ToList()
                .FirstOrDefault();
        }

        public Roles GetRoleFromRUID(int RUID)
        {
            return _context.Roles.Where(r => r.Ruid == RUID).FirstOrDefault();
        }

        public User GetUser(Guid UUID)
        {
            var u = _context.Users.Where(e => e.Uuid.Equals(UUID) && !e.DeletedFlag).FirstOrDefault();
            if (u != null)
            {
                return new User
                {
                    Username = u.Username,
                    Name = u.Name,
                    Surname = u.Surname
                };
            }
            else
                return null;
        }
        [Obsolete]
        public Users GetUserFromSignInKey(string SignInKey)
        {
            return _context.Users
                .FromSql($"SelectUserFromSignInKey {SignInKey}")
                .ToList()
                .FirstOrDefault();
        }
        [Obsolete]
        public EntityDetails InsertDetailForEntityOfFP(EntityDetails detail)
        {
            return _context.EntityDetails
                .FromSql($"InsertDetailForEntityOfFP {detail.Euid}, {detail.Name}, {detail.Description}, {detail.Cost}, {detail.RemainderDate}, {detail.CreatedByUuid}")
                .ToList()
                .FirstOrDefault();
        }
        [Obsolete]
        public EntityCopvalues InsertEntityCOPValue(EntityCopvalues copvalue)
        {
            return _context.EntityCopvalues
                .FromSql($"InsertEntityCOPValue {copvalue.Euid}, {copvalue.Puid}, {copvalue.Value}")
                .ToList()
                .FirstOrDefault();
        }
        [Obsolete]
        public EntityOfFp InsertEntityForFP(EntityOfFp entity)
        {
            return _context.EntityOfFp
                .FromSql($"InsertEntityForFP {entity.Cuid}, {entity.Puid}, {entity.Id}, {entity.Name}, {entity.Description}, {entity.Count}, {entity.PurchasedDate}, {entity.Cost}, {entity.CreatedByUuid}")
                .ToList()
                .FirstOrDefault();
        }
        [Obsolete]
        public IncomeAndExpeneses InsertExpense(IncomeAndExpeneses expenese)
        {
            return _context.IncomeAndExpeneses
                .FromSql($"InsertExpense {expenese.Fuid}, {expenese.Date}, {expenese.Head}, {expenese.Decription}, {expenese.Cost}, {expenese.CreatedByUuid}")
                .ToList()
                .FirstOrDefault();
        }
        [Obsolete]
        public Farms InsertFarm(Farms farm)
        {
            return _context.Farms
                .FromSql($"InsertFarm {farm.Name}, {farm.Description}, {farm.CreatedByUuid}")
                .ToList()
                .FirstOrDefault();
        }
        [Obsolete]
        public FarmProperties InsertFarmProperty(FarmProperties property)
        {
            return _context.FarmProperties
                .FromSql($"InsertFarmProperty {property.Name}, {property.Description}, {property.Tuid}, {property.Fuid}, {property.CreatedByUuid}")
                .ToList()
                .FirstOrDefault();
        }
        [Obsolete]
        public IncomeAndExpeneses InsertIncome(IncomeAndExpeneses income)
        {
            return _context.IncomeAndExpeneses
                .FromSql($"InsertIncome {income.Fuid}, {income.Date}, {income.Head}, {income.Decription}, {income.Cost}, {income.CreatedByUuid}")
                .ToList()
                .FirstOrDefault();
        }

        [Obsolete]
        public Sessions InsertSession(Guid UUID)
        {
            return _context.Sessions
                .FromSql($"InsertSession {UUID}")
                .ToList()
                .FirstOrDefault();
        }

        public void InsertSignInLog(SignInLogs log)
        {
            _context.SignInLogs.Add(log);
        }

        [Obsolete]
        public GeneratedUcodes InsertUser(Users User, Guid UCode)
        {
            return _context.GeneratedUcodes
                .FromSql($"InsertUser {User.Username}, {User.Password}, {User.Email}, {User.Name}, {User.Surname}, {UCode}")
                .ToList()
                .FirstOrDefault();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
