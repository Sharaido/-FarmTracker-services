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
    [Authorize]
    public class FarmsController : ControllerBase
    {
        private readonly IFarmTrackerRepo _repositroy;

        public FarmsController(IFarmTrackerRepo repository)
        {
            _repositroy = repository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Farms>> Get()
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            var r = _repositroy.GetAllFarmsForUUID(UUID);
            if (r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        } 
        [HttpGet("{FUID}")]
        public ActionResult<Farms> Get(Guid FUID)
        {
            var r = _repositroy.GetFarm(FUID);
            if (r == null)
            {
                return NotFound();
            }
            return Ok(r);
        } 
        [HttpPost]
        public ActionResult<Farms> Post([FromBody] Farms farm)
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            farm.CreatedByUuid = UUID;
            var r = _repositroy.InsertFarm(farm);
            if (r == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(
                nameof(Get),
                new {FUID = r.Fuid},
                new
                {
                    FUID = r.Fuid,
                    Name = r.Name,
                    Description = r.Description
                }
                );
        }
        [HttpGet("Properties/{FUID}")]
        public ActionResult<IEnumerable<FarmProperties>> GetFarmProperties(Guid FUID)
        {
            var r = _repositroy.GetFarmProperties(FUID);
            if (r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpGet("Properties/{FUID}/{PUID}")]
        public ActionResult<FarmProperties> GetFarmProperty(Guid FUID, Guid PUID)
        {
            var r = _repositroy.GetFarmProperty(FUID, PUID);
            if (r == null)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpPost("Properties")]
        public ActionResult<FarmProperties> InsertFarmProperty([FromBody] FarmProperties property)
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            property.CreatedByUuid = UUID;
            var r = _repositroy.InsertFarmProperty(property);
            if (r == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(
                nameof(GetFarmProperty),
                new 
                { 
                    FUID = r.Fuid,
                    PUID = r.Puid 
                },
                new
                {
                    FUID = r.Fuid,
                    PUID = r.Puid,
                    TUID = r.Tuid,
                    Name = r.Name,
                    Description = r.Description
                }
                );
        }
        [HttpGet("Properties/Entities/{PUID}")]
        public ActionResult<IEnumerable<EntityOfFp>> GetEntitiesOfFP(Guid PUID)
        {
            var r = _repositroy.GetEntitiesOfFP(PUID);
            if (r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpGet("Properties/Entities/{PUID}/{EUID}")]
        public ActionResult<FarmProperties> GetEntitiesOfFP(Guid PUID, Guid EUID)
        {
            var r = _repositroy.GetEntityOfFP(PUID, EUID);
            if (r == null)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpPost("Properties/Entities/")]
        public ActionResult<EntityOfFp> InsertEntityOfFP([FromBody] EntityOfFp entity)
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            entity.CreatedByUuid = UUID;
            var r = _repositroy.InsertEntityForFP(entity);
            if (r == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(
                nameof(GetEntitiesOfFP),
                new
                {
                    PUID = r.Puid,
                    EUID = r.Euid
                },
                new
                {
                    EUID = r.Euid,
                    PUID = r.Puid,
                    CUID = r.Cuid,
                    ID = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Count = r.Count,
                    PurchasedDate = r.PurchasedDate,
                    Cost = r.Cost
                }
                );
        }
    }
}
