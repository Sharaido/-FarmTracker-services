﻿using FarmTracker_services.Models.DB;
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
        [Obsolete]
        public IEnumerable<CategoryOfProperties> GetCategoryProperties(int CUID)
        {
            // This function must be improved
            var ls = _context.CategoryOfProperties
               .FromSql($"SelectPropertiesOfTheCategory {CUID}")
               .ToList();
            
            ls.ForEach(e => e.Tu = _context.Coptypes
                .Where(x => x.Tuid == e.Tuid)
                .FirstOrDefault());

            return ls;
        }

        public EntityCopvalues GetECOPValue(Guid EUID, int PUID)
        {
            return _context.EntityCopvalues
                .Where(e => e.Euid == EUID && e.Puid == PUID)
                .FirstOrDefault();
        }

        public IEnumerable<EntityCopvalues> GetECOPValues(Guid EUID)
        {
            return _context.EntityCopvalues
                .Where(e => e.Euid == EUID);
        }

        public IEnumerable<Copvalues> GetCOPValues(int PUID)
        {
            return _context.Copvalues
                .Where(e => e.Puid == PUID);
        }

        public IEnumerable<EntityOfFp> GetEntitiesOfFP(Guid PUID)
        {
            return _context.EntityOfFp
                .Where(e => e.Puid == PUID && !e.DeletedFlag)
                .OrderByDescending(e => e.LastModifiedDate)
                .ThenByDescending(e => e.CreatedDate);
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
                .Where(e => e.Euid == EUID && !e.DeletedFlag)
                .OrderByDescending(e => e.CreatedDate);
        }

        public EntityOfFp GetEntityOfFP(Guid PUID, Guid EUID)
        {
            return _context.EntityOfFp
                .Where(e => e.Puid == PUID && e.Euid == EUID && !e.DeletedFlag)
                .FirstOrDefault();
        }

        public IEnumerable<IncomeAndExpeneses> GetExpenses(Guid FUID)
        {
            return _context.IncomeAndExpeneses
                .Where(e => e.Fuid == FUID && !e.DeletedFlag && !e.IncomeFlag)
                .OrderByDescending(e => e.CreatedDate);
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
            return _context.FarmProperties.Where(e => e.Fuid == FUID && !e.DeletedFlag)
                .OrderByDescending(e => e.LastModifiedDate)
                .ThenByDescending(e => e.CreatedDate);
        }

        public FarmProperties GetFarmProperty(Guid FUID, Guid PUID)
        {
            return _context.FarmProperties.Where(e => e.Puid == PUID && e.Fuid == FUID && !e.DeletedFlag).FirstOrDefault();
        }

        public IEnumerable<IncomeAndExpeneses> GetIncomeAndExpenses(Guid FUID)
        {
            return _context.IncomeAndExpeneses
                .Where(e => e.Fuid == FUID && !e.DeletedFlag)
                .OrderByDescending(e => e.CreatedDate);
        }

        public IncomeAndExpeneses GetIncomeAndExpenses(Guid FUID, Guid IEUID)
        {
            return _context.IncomeAndExpeneses
                .Where(e => e.Fuid == FUID && e.Ieuid == IEUID)
                .FirstOrDefault();
        }

        public IEnumerable<IncomeAndExpeneses> GetIncomes(Guid FUID)
        {
            return _context.IncomeAndExpeneses
                .Where(e => e.Fuid == FUID && !e.DeletedFlag && e.IncomeFlag)
                .OrderByDescending(e => e.CreatedDate);
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

        public Sessions GetSession(Guid SUID, Guid UUID)
        {
            return _context.Sessions
                .Where(e => e.Suid == SUID && e.Uuid == UUID && e.IsValid)
                .FirstOrDefault();
        }

        public IEnumerable<Categories> GetSubCategoies(int CUID)
        {
            return _context.Categories
                .Where(e => e.SubCategoryOfCuid == CUID)
                .OrderBy(e => e.Name);
        }

        public Users GetUser(Guid UUID)
        {
            var u = _context.Users.Where(e => e.Uuid.Equals(UUID) && !e.DeletedFlag).FirstOrDefault();
            if (u != null)
            {
                return new Users
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
            var user =  _context.Users
                .FromSql($"SelectUserFromSignInKey {SignInKey}")
                .ToList()
                .FirstOrDefault();
            user.Collaborators = _context.Collaborators.Where(e => e.Uuid == user.Uuid).ToList();
            user.FarmsCreatedByUu = _context.Farms.Where(e => e.CreatedByUuid == user.Uuid).ToList();
            return user;
        }

        public Users GetUserFromUUID(Guid UUID)
        {
            return _context.Users
                .Where(e => e.Uuid == UUID && !e.DeletedFlag)
                .FirstOrDefault();
        }

        public void InactivateSession(Guid SUID)
        {
            Sessions s = _context.Sessions.Where(e => e.Suid == SUID).FirstOrDefault();
            s.LastUsedDate = DateTime.UtcNow;
            s.IsValid = false;
            _context.SaveChanges();
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
                .FromSql($"InsertExpense {expenese.Fuid}, {expenese.Date}, {expenese.Head}, {expenese.Description}, {expenese.Cost}, {expenese.CreatedByUuid}")
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
                .FromSql($"InsertFarmProperty {property.Name}, {property.Description}, {property.Cuid}, {property.Fuid}, {property.CreatedByUuid}")
                .ToList()
                .FirstOrDefault();
        }
        [Obsolete]
        public IncomeAndExpeneses InsertIncome(IncomeAndExpeneses income)
        {
            return _context.IncomeAndExpeneses
                .FromSql($"InsertIncome {income.Fuid}, {income.Date}, {income.Head}, {income.Description}, {income.Cost}, {income.CreatedByUuid}")
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

        public bool IsUsedEmail(string Email)
        {
            return (_context.Users.Where(e => e.Email == Email && !e.DeletedFlag).FirstOrDefault() != null);
        }

        public bool IsUsedUsername(string Username)
        {
            return (_context.Users.Where(e => e.Username == Username && !e.DeletedFlag).FirstOrDefault() != null);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void UpdateSessionLastUsed(Guid SUID)
        {
            Sessions s = _context.Sessions.Where(e => e.Suid == SUID).FirstOrDefault();
            s.LastUsedDate = DateTime.UtcNow;
            _context.SaveChanges();
        }

        [Obsolete]
        public bool DeleteFarm(Guid FUID, Guid UUID)
        {
            var r = _context.Database
                .ExecuteSqlCommand($"DeleteFarm {FUID}, {UUID}");
            if (r > 0)
            {
                return true;
            }
            return false;
        }

        [Obsolete]
        public bool DeleteFarmProperty(Guid PUID, Guid UUID)
        {
            var r = _context.Database
                .ExecuteSqlCommand($"DeleteFarmProperty {PUID}, {UUID}");
            if (r > 0)
            {
                return true;
            }
            return false;
        }
        [Obsolete]
        public bool DeleteFPEntity(Guid EUID, Guid UUID)
        {
            var r = _context.Database
                .ExecuteSqlCommand($"DeleteFPEntity {EUID}, {UUID}");
            if (r > 0)
            {
                return true;
            }
            return false;
        }

        [Obsolete]
        public bool DeleteIncomeAndExpenses(Guid IEUID, Guid UUID)
        {
            var r = _context.Database
                .ExecuteSqlCommand($"DeleteIncomeAndExpenses {IEUID}, {UUID}");
            if (r > 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<FarmEntities> GetFarmEntities(Guid FUID)
        {
            return _context.FarmEntities
                .Where(e => !e.DeletedFlag && e.Fuid == FUID)
                .OrderByDescending(e => e.CreatedDate);
        }
        [Obsolete]
        public FarmEntities InsertFarmEntity(FarmEntities entity)
        {
            return _context.FarmEntities
                .FromSql($"InsertFarmEntity {entity.Name}, {entity.Cuid}, {entity.Fuid}, {entity.Count}, {entity.CreatedByUuid}")
                .ToList()
                .FirstOrDefault();
        }
        [Obsolete]
        public bool DeleteFarmEntity(Guid EUID, Guid UUID)
        {
            var r = _context.Database
                .ExecuteSqlCommand($"DeleteFarmEntity {EUID}, {UUID}");
            if (r > 0)
            {
                return true;
            }
            return false;
        }

        public bool UpdateMemberType(Guid UUID, int MTUID)
        {
            Users user = _context.Users.Where(e => e.Uuid == UUID).FirstOrDefault();
            if (user !=null )
            {
                user.Mtuid = MTUID;
                var r = _context.SaveChanges();
                if (r > 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public IEnumerable<Collaborators> GetCollaboratorsForFUID(Guid FUID)
        {
            var collaborators = _context.Collaborators.Where(e => e.Fuid == FUID);
            foreach (var item in collaborators)
            {
                item.Uu = _context.Users.Where(e => e.Uuid == item.Uuid).FirstOrDefault();
            }
            return collaborators;
        }
        public Collaborators AddOrUpdateCollaboratorToFarm(Collaborators collaborator)
        {
            Users user = _context.Users.Where(e => e.Uuid == collaborator.Uuid).FirstOrDefault();
            Farms farm = _context.Farms.Where(e => e.Fuid == collaborator.Fuid).FirstOrDefault();
            if (user == null || farm == null)
            {
                return null;
            }
            if (user.Uuid.Equals(farm.CreatedByUuid))
            {
                return null;
            }
            Collaborators _collaborator = _context.Collaborators.Where(e => e.Uuid == collaborator.Uuid && e.Fuid == collaborator.Fuid).FirstOrDefault();
            if (_collaborator != null)
            {
                _collaborator.Ruid = collaborator.Ruid;
            }
            else
            {
                _context.Collaborators.Add(collaborator);
            }

            var r = _context.SaveChanges();
            if (r > 0)
            {
                return collaborator;
            }
            return null;
        }

        public bool DeleteCollaborator(Collaborators collaborator)
        {
            Collaborators _collaborator = _context.Collaborators.Where(e => e.Uuid == collaborator.Uuid && e.Fuid == collaborator.Fuid).FirstOrDefault();
            if (_collaborator != null)
            {
                _context.Collaborators.Remove(_collaborator);
            }
            var r = _context.SaveChanges();
            if (r > 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<CRoles> GetCRoles()
        {
            return _context.CRoles.OrderBy(e => e.Name);
        }

        public IEnumerable<Users> SearchUser(string key)
        {
            var users = _context.Users.Where(e => e.Name.Contains(key) || e.Surname.Contains(key) || e.Username.Contains(key));
                
                /*from u in _context.Users
                        where EF.Functions.Like(u.Name, "%"+key+"%")
                        select u;*/

            return users;
        }
    }
}
