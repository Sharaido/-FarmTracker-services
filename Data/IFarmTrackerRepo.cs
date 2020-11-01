using FarmTracker_services.Models.DB;
using FarmTracker_services.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmTracker_services.Data
{
    public interface IFarmTrackerRepo
    {
        Users GetUserFromSignInKey(string SignInKey);
        GeneratedUcodes InsertUser(Users User, Guid UCode);
        User GetUser(Guid UUID);
        GeneratedUcodes GetNewUCodeForSignUp(string ip);
        void InsertSignInLog(SignInLogs log);
        int GetFailedSignInRequestsInTheLast5MinFromUUID(Guid UUID);
        bool SaveChanges();
        Sessions InsertSession(Guid UUID);
        Roles GetRoleFromRUID(int RUID);
        Farms InsertFarm(Farms farm);
        FarmProperties InsertFarmProperty(FarmProperties property);
        EntityOfFp InsertEntityForFP(EntityOfFp entity);
        EntityDetails InsertDetailForEntityOfFP(EntityDetails detail);
        EntityCopvalues InsertEntityCOPValue(EntityCopvalues copvalue);
        IncomeAndExpeneses InsertIncome(IncomeAndExpeneses income);
        IncomeAndExpeneses InsertExpense(IncomeAndExpeneses expenese);
        IEnumerable<Farms> GetAllFarmsForUUID(Guid UUID);
        Farms GetFarm(Guid FUID);
        IEnumerable<FarmProperties> GetFarmProperties(Guid FUID);
        FarmProperties GetFarmProperty(Guid FUID, Guid PUID);
        IEnumerable<EntityOfFp> GetEntitiesOfFP(Guid PUID);
        EntityOfFp GetEntityOfFP(Guid PUID, Guid EUID);
        IEnumerable<EntityCopvalues> GetCOPValues(Guid EUID);
        EntityCopvalues GetCOPValue(Guid EUID, int PUID);

    }
}
