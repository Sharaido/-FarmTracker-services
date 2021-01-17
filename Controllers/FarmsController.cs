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
            if (r == null || r.Count() == 0)
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
            if (r == null || r.Count() == 0)
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
            return Ok(r);
        }
        [HttpGet("Properties/Entities/{PUID}")]
        public ActionResult<IEnumerable<EntityOfFp>> GetEntitiesOfFP(Guid PUID)
        {
            var r = _repositroy.GetEntitiesOfFP(PUID);
            if (r == null || r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpGet("Properties/Entities/{PUID}/{EUID}")]
        public ActionResult<EntityOfFp> GetEntitiesOfFP(Guid PUID, Guid EUID)
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
            return Ok(r);
        }
        [HttpGet("Properties/Entities/COPValues/{EUID}")]
        public ActionResult<IEnumerable<EntityCopvalues>> GetECOPValues(Guid EUID)
        {
            var r = _repositroy.GetECOPValues(EUID);
            if (r == null || r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpGet("Properties/Entities/COPValues/{EUID}/{PUID}")]
        public ActionResult<EntityCopvalues> GetECOPValues(Guid EUID, int PUID )
        {
            var r = _repositroy.GetECOPValue(EUID, PUID);
            if (r == null)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpPost("Properties/Entities/COPValues/")]
        public ActionResult<EntityCopvalues> InsertEntityCOPValue([FromBody] EntityCopvalues value)
        {
            var r = _repositroy.InsertEntityCOPValue(value);
            if (r == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(
                nameof(GetECOPValues),
                new
                {
                    EUID = r.Euid,
                    PUID = r.Puid
                },
                new
                {
                    EUID = r.Euid,
                    PUID = r.Puid,
                    value = r.Value
                }
                );
        }
        [HttpGet("Properties/Entities/Details/{EUID}")]
        public ActionResult<IEnumerable<EntityDetails>> GetEntityDetails(Guid EUID)
        {
            var r = _repositroy.GetEntityDetails(EUID);
            if (r == null || r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpGet("Properties/Entities/Details/{EUID}/{DUID}")]
        public ActionResult<EntityDetails> GetEntityDetails(Guid EUID, Guid DUID)
        {
            var r = _repositroy.GetEntityDetail(EUID, DUID);
            if (r == null)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpPost("Properties/Entities/Details/")]
        public ActionResult<EntityDetails> InsertDetailForEntityOfFP([FromBody] EntityDetails detail)
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            var r = _repositroy.InsertOrUpdateEntityDetail(detail , UUID);
            if (r == null)
            {
                return BadRequest();
            }
            return Ok(r);
        }
        [HttpGet("IncomeAndExpenses/{FUID}")]
        public ActionResult<IEnumerable<IncomeAndExpeneses>> GetIncomeAndExpenses(Guid FUID)
        {
            var r = _repositroy.GetIncomeAndExpenses(FUID);
            if (r == null || r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpGet("Incomes/{FUID}")]
        public ActionResult<IEnumerable<IncomeAndExpeneses>> GetIncomes(Guid FUID)
        {
            var r = _repositroy.GetIncomes(FUID);
            if (r == null || r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpGet("Expenses/{FUID}")]
        public ActionResult<IEnumerable<IncomeAndExpeneses>> GetExpenses(Guid FUID)
        {
            var r = _repositroy.GetExpenses(FUID);
            if (r == null || r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpGet("IncomeAndExpenses/{FUID}/{IEUID}")]
        public ActionResult<IncomeAndExpeneses> GetIncomeAndExpenses(Guid FUID, Guid IEUID)
        {
            var r = _repositroy.GetIncomeAndExpenses(FUID, IEUID);
            if (r == null)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpPost("Incomes/")]
        public ActionResult<IncomeAndExpeneses> InsertIncome([FromBody] IncomeAndExpeneses income)
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            income.CreatedByUuid = UUID;
            var r = _repositroy.InsertIncome(income);
            if (r == null)
            {
                return BadRequest();
            }
            return Ok(r);
        }
        [HttpPost("Expenses/")]
        public ActionResult<IncomeAndExpeneses> InsertExpenses([FromBody] IncomeAndExpeneses expenese)
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            expenese.CreatedByUuid = UUID;
            var r = _repositroy.InsertExpense(expenese);
            if (r == null)
            {
                return BadRequest();
            }
            return Ok(r);
        }
        [HttpGet("SubCategories/{CUID}")]
        public ActionResult<IEnumerable<Categories>> GetSubCategories(int CUID)
        {
            var r = _repositroy.GetSubCategoies(CUID);
            if (r == null || r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpGet("CategoryProperties/{CUID}")]
        public ActionResult<IEnumerable<CategoryOfProperties>> GetCategoryProperties(int CUID)
        {
            var r = _repositroy.GetCategoryProperties(CUID);
            if (r == null || r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpGet("COPValues/{PUID}")]
        public ActionResult<IEnumerable<CategoryOfProperties>> GetCOPValues(int PUID)
        {
            var r = _repositroy.GetCOPValues(PUID);
            if (r == null || r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpDelete("Properties/Entities/{EUID}")]
        public ActionResult<bool> DeleteFPEntities(Guid EUID)
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            var r = _repositroy.DeleteFPEntity(EUID, UUID);
            return Ok(r);
        }
        [HttpDelete("Properties/{PUID}")]
        public ActionResult<bool> DeleteFarmProperty(Guid PUID)
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            var r = _repositroy.DeleteFarmProperty(PUID, UUID);
            return Ok(r);
        }
        [HttpDelete("{FUID}")]
        public ActionResult<bool> Delete(Guid FUID)
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            var r = _repositroy.DeleteFarm(FUID, UUID);
            return Ok(r);
        }
        [HttpDelete("IncomeAndExpenses/{IEUID}")]
        public ActionResult<bool> DeleteIncomeAndExpenses(Guid IEUID)
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            var r = _repositroy.DeleteIncomeAndExpenses(IEUID, UUID);
            return Ok(r);
        }

        [HttpGet("FarmEntities/{FUID}")]
        public ActionResult<IEnumerable<FarmEntities>> GetFarmEntities(Guid FUID)
        {
            var r = _repositroy.GetFarmEntities(FUID);
            if (r == null || r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpPost("FarmEntities/")]
        public ActionResult<FarmEntities> InsertFarmEntity([FromBody] FarmEntities entity)
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            entity.CreatedByUuid = UUID;
            var r = _repositroy.InsertFarmEntity(entity);
            if (r == null)
            {
                return BadRequest();
            }
            return Ok(r);
        }
        [HttpDelete("FarmEntities/{EUID}")]
        public ActionResult<bool> DeleteFarmEntity(Guid EUID)
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            var r = _repositroy.DeleteFarmEntity(EUID, UUID);
            return Ok(r);
        }

        [HttpGet("Collaborators/{FUID}")]
        public ActionResult<IEnumerable<Collaborators>> GetCollaboratorsForFUID(Guid FUID)
        {
            var r = _repositroy.GetCollaboratorsForFUID(FUID);
            if (r == null || r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpPost("Collaborators/")]
        public ActionResult<Collaborators> InsertCollaborator([FromBody] Collaborators collaborator)
        {
            var r = _repositroy.AddOrUpdateCollaboratorToFarm(collaborator);
            if (r != null)
            {
                return Ok(r);
            }
            return BadRequest();
        }
        [HttpDelete("Collaborators/")]
        public ActionResult<bool> DeleteFarmEntity(Collaborators collaborator)
        {
            var r = _repositroy.DeleteCollaborator(collaborator);
            if (r)
            {
                return Ok(r);
            }
            return BadRequest();
        }
        [HttpGet("CollaboratorRoles/")]
        public ActionResult<IEnumerable<CRoles>> GetCRoles()
        {
            var r = _repositroy.GetCRoles();
            if (r == null || r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }

        [HttpDelete("Properties/Entities/Details/{DUID}")]
        public ActionResult<bool> DeleteEntityDetail(Guid DUID)
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            var r = _repositroy.DeleteEntityDetail(DUID, UUID);
            if (r)
            {
                return Ok(r);
            }
            return BadRequest(r);
        }



        [HttpGet("GetRemainders/")]
        public ActionResult<IEnumerable<EntityDetails>> GetRemainders()
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            var r = _repositroy.GetRemaindersForUUID(UUID);
            if (r == null || r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }


        [HttpGet("Properties/Details/{PUID}")]
        public ActionResult<IEnumerable<PropertyDetail>> GetPDetails(Guid PUID)
        {
            var r = _repositroy.GetPropertyDetails(PUID);
            if (r == null || r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpPost("Properties/Details/")]
        public ActionResult<PropertyDetail> InsertOrUpdatePDetail([FromBody] PropertyDetail detail)
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            var r = _repositroy.InsertOrUpdatePDetail(detail, UUID);
            if (r == null)
            {
                return BadRequest();
            }
            return Ok(r);
        }
        [HttpDelete("Properties/Details/{DUID}")]
        public ActionResult<bool> DeletePDetail(Guid DUID)
        {
            var UUID = new Guid(User.Claims.FirstOrDefault(e => e.Type.Equals("UUID")).Value);
            var r = _repositroy.DeletePropertyDetail(DUID, UUID);
            if (r)
            {
                return Ok(r);
            }
            return BadRequest(r);
        }
    }
}
