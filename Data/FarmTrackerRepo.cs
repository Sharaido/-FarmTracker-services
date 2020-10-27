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
        public int GetFailedSignInRequestsInTheLast5MinFromUUID(Guid UUID)
        {
            return _context.SignInLogs
                .FromSql($"SelectFailedSignInRequestsInTheLast5MinFromUUID {UUID}")
                .ToList()
                .Count;
        }

        [Obsolete]
        public GeneratedUcodes GetNewUCodeForSignUp(string ip)
        {
            return _context.GeneratedUcodes
                .FromSql($"InsertUCodeForSignUp {ip}")
                .ToList()
                .FirstOrDefault();
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
