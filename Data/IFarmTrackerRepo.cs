using FarmTracker_services.Models.DB;
using FarmTracker_services.Models.Members;
using System;
using System.Collections.Generic;

namespace FarmTracker_services.Data
{
    public interface IFarmTrackerRepo
    {
        Users GetUserFromSignInKey(string SignInKey);
        Users GetUserFromUUID(Guid UUID);
        GeneratedUcodes InsertUser(Users User, Guid UCode);
        Users GetUser(Guid UUID);
        GeneratedUcodes GetNewUCodeForSignUp(string ip);
        void InsertSignInLog(SignInLogs log);
        int GetFailedSignInRequestsInTheLast5MinFromUUID(Guid UUID);
        bool SaveChanges();
        Sessions InsertSession(Guid UUID);
        Roles GetRoleFromRUID(int RUID);
        Farms InsertFarm(Farms farm);
        FarmProperties InsertFarmProperty(FarmProperties property);
        EntityOfFp InsertEntityForFP(EntityOfFp entity);
        EntityDetails InsertOrUpdateEntityDetail(EntityDetails detail, Guid UUID);
        EntityCopvalues InsertEntityCOPValue(EntityCopvalues copvalue);
        IncomeAndExpeneses InsertIncome(IncomeAndExpeneses income);
        IncomeAndExpeneses InsertExpense(IncomeAndExpeneses expenese);
        IEnumerable<Farms> GetAllFarmsForUUID(Guid UUID);
        Farms GetFarm(Guid FUID);
        IEnumerable<FarmProperties> GetFarmProperties(Guid FUID);
        FarmProperties GetFarmProperty(Guid FUID, Guid PUID);
        IEnumerable<EntityOfFp> GetEntitiesOfFP(Guid PUID);
        EntityOfFp GetEntityOfFP(Guid PUID, Guid EUID);
        IEnumerable<EntityCopvalues> GetECOPValues(Guid EUID);
        EntityCopvalues GetECOPValue(Guid EUID, int PUID);
        IEnumerable<EntityDetails> GetEntityDetails(Guid EUID);
        EntityDetails GetEntityDetail(Guid EUID, Guid DUID);
        IEnumerable<IncomeAndExpeneses> GetIncomeAndExpenses(Guid FUID);
        IEnumerable<IncomeAndExpeneses> GetIncomes(Guid FUID);
        IEnumerable<IncomeAndExpeneses> GetExpenses(Guid FUID);
        IncomeAndExpeneses GetIncomeAndExpenses(Guid FUID, Guid IEUID);
        Sessions GetSession(Guid SUID, Guid UUID);
        void UpdateSessionLastUsed(Guid SUID);
        void InactivateSession(Guid SUID);
        bool IsUsedUsername(string Username);
        bool IsUsedEmail(string Email);
        IEnumerable<Categories> GetSubCategoies(int CUID);
        IEnumerable<CategoryOfProperties> GetCategoryProperties(int CUID);
        IEnumerable<Copvalues> GetCOPValues(int PUID);
        bool DeleteFarm(Guid FUID, Guid UUID);
        bool DeleteFarmProperty(Guid PUID, Guid UUID);
        bool DeleteFPEntity(Guid EUID, Guid UUID);
        bool DeleteIncomeAndExpenses(Guid IEUID, Guid UUID);
        IEnumerable<FarmEntities> GetFarmEntities(Guid FUID);
        FarmEntities InsertFarmEntity(FarmEntities entity);
        bool DeleteFarmEntity(Guid EUID, Guid UUID);
        bool UpdateMemberType(Guid UUID, int MTUID);
        IEnumerable<Collaborators> GetCollaboratorsForFUID(Guid FUID);
        Collaborators AddOrUpdateCollaboratorToFarm(Collaborators collaborator);
        bool DeleteCollaborator(Collaborators collaborator);
        IEnumerable<CRoles> GetCRoles();
        IEnumerable<Users> SearchUser(string key);
        bool DeleteEntityDetail(Guid DUID, Guid UUID);
        IEnumerable<EntityDetails> GetRemaindersForUUID(Guid UUID);

        
        PropertyDetail InsertOrUpdatePDetail(PropertyDetail detail, Guid UUID);
        IEnumerable<PropertyDetail> GetPropertyDetails(Guid PUID);
        bool DeletePropertyDetail(Guid DUID, Guid UUID);
        //IEnumerable<PropertyDetail> GetPropertyRemaindersForUUID(Guid UUID);
    }
}
