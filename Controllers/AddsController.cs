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
        private readonly IFarmTrackerRepo _repositroyFarmTracker;

        public AddsController(IAddsRepo repository, IFarmTrackerRepo repositroyFarmTracker)
        {
            _repositroy = repository;
            _repositroyFarmTracker = repositroyFarmTracker;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Adds>> Get()
        {
            var r = _repositroy.GetAdds();
            if (r.Count() == 0)
            {
                return NotFound();
            }
            foreach (var i in r)
            {
                var pictures = _repositroy.GetPicturesForAdd(i.Auid);
                i.Pictures = pictures.ToList();

                var copValues = _repositroy.GetACopValues(i.Auid);
                i.AddCopvalues = copValues.ToList();

                var ownerUser = _repositroyFarmTracker.GetUserFromUUID((Guid)i.CreatedByUuid);
                i.CreatedByUu = ownerUser;
            }
            return Ok(r);
        }
        [HttpGet("{AUID}")]
        public ActionResult<Adds> GetAdd(Guid AUID)
        {
            var r = _repositroy.GetAdds(AUID);
            if (r == null)
            {
                return NotFound();
            }
            var pictures = _repositroy.GetPicturesForAdd(r.Auid);
            r.Pictures = pictures.ToList();

            var copValues = _repositroy.GetACopValues(r.Auid);
            r.AddCopvalues = copValues.ToList();

            var ownerUser = _repositroyFarmTracker.GetUserFromUUID((Guid)r.CreatedByUuid);
            r.CreatedByUu = ownerUser;

            return Ok(r);
        }
        [HttpGet("User/{UUID}")]
        public ActionResult<IEnumerable<Adds>> GetUserAdds(Guid UUID)
        {
            var r = _repositroy.GetUserAdds(UUID);
            if (r.Count() == 0)
            {
                return NotFound();
            }
            foreach (var i in r)
            {
                var pictures = _repositroy.GetPicturesForAdd(i.Auid);
                i.Pictures = pictures.ToList();
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
        [HttpPost("Pictures/")]
        [Authorize]
        public ActionResult<bool> InsertPictureForAdd([FromBody] Pictures picture)
        {
            return Ok(_repositroy.InsertPictureForAdd(picture));
        }
        [HttpGet("Pictures/{AUID}")]
        public ActionResult<IEnumerable<Pictures>> GetPicturesForAdd(Guid AUID)
        {
            var r = _repositroy.GetPicturesForAdd(AUID);
            if (r == null || r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }

        [HttpGet("COPValues/{AUID}")]
        public ActionResult<IEnumerable<EntityCopvalues>> GetECOPValues(Guid AUID)
        {
            var r = _repositroy.GetACopValues(AUID);
            if (r == null || r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpPost("COPValues/")]
        [Authorize]
        public ActionResult<bool> InsertEntityCOPValue([FromBody] AddCopvalues value)
        {
            var r = _repositroy.InsertACopValue(value);
            return Ok(r);
        }
    }
}
