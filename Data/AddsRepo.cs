using FarmTracker_services.Models.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmTracker_services.Data
{
    public class AddsRepo : IAddsRepo
    {
        private readonly farmTrackerContext _context;

        public AddsRepo(farmTrackerContext context)
        {
            _context = context;
        }

        public IEnumerable<Adds> GetAdds()
        {
            return _context.Adds
                .Where(e => !e.DeletedFlag)
                .OrderByDescending(e => e.CreatedDate);
        }

        public IEnumerable<Adds> GetUserAdds(Guid UUID)
        {
            return _context.Adds
                .Where(e => !e.DeletedFlag && e.CreatedByUuid == UUID)
                .OrderByDescending(e => e.CreatedDate);
        }

        public Adds GetAdds(Guid AUID)
        {
            return _context.Adds
                .Where(e => !e.DeletedFlag && e.Auid == AUID)
                .FirstOrDefault();
        }
        [Obsolete]
        public Adds InsertAdds(Adds add)
        {
            return _context.Adds
                .FromSql($"InsertAdd {add.Name}, {add.Description}, {add.Price}, {add.CreatedByUuid}, {add.Cuid}")
                .ToList()
                .FirstOrDefault();
        }
        [Obsolete]
        public bool DeleteAdds(Guid AUID, Guid UUID)
        {
            var r = _context.Database
                .ExecuteSqlCommand($"DeleteAdd {AUID}, {UUID}");
            if (r > 0)
            {
                return true;
            }
            return false;
        }
    }
}
