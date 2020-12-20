using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmTracker_services.Data;
using FarmTracker_services.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmTracker_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddsController : ControllerBase
    {
        private readonly IAddsRepo _repositroy;

        public AddsController(IAddsRepo repository)
        {
            _repositroy = repository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Adds>> Get()
        {
            var r = _repositroy.GetAdds();
            if (r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpGet("{UUID}")]
        public ActionResult<IEnumerable<Adds>> GetUserAdds(Guid UUID)
        {
            var r = _repositroy.GetUserAdds(UUID);
            if (r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpPost]
        [Authorize]
        public ActionResult<Adds> InsertAdd([FromBody] Adds add)
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            add.CreatedByUuid = UUID;
            var r = _repositroy.InsertAdds(add);
            if (r == null)
            {
                return BadRequest();
            }
            return Ok(r);
        }
        [HttpDelete("{AUID}")]
        [Authorize]
        public ActionResult<bool> DeleteAdd(Guid AUID)
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            var r = _repositroy.DeleteAdds(AUID, UUID);
            return Ok(r);
        }
    }
}
