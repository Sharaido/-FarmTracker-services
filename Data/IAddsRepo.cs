using FarmTracker_services.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmTracker_services.Data
{
    public interface IAddsRepo
    {
        IEnumerable<Adds> GetAdds();
        IEnumerable<Adds> GetUserAdds(Guid UUID);
        Adds GetAdds(Guid AUID);
        Adds InsertAdds(Adds add);
        bool DeleteAdds(Guid AUID, Guid UUID);
    }
}
