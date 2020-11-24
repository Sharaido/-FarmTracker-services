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
                    CUID = r.Cuid,
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
        [HttpGet("Properties/Entities/COPValues/{EUID}")]
        public ActionResult<IEnumerable<EntityOfFp>> GetECOPValues(Guid EUID)
        {
            var r = _repositroy.GetECOPValues(EUID);
            if (r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpGet("Properties/Entities/COPValues/{EUID}/{PUID}")]
        public ActionResult<FarmProperties> GetECOPValues(Guid EUID, int PUID )
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
            if (r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpGet("Properties/Entities/Details/{EUID}/{DUID}")]
        public ActionResult<FarmProperties> GetEntityDetails(Guid EUID, Guid DUID)
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
            detail.CreatedByUuid = UUID;
            var r = _repositroy.InsertDetailForEntityOfFP(detail);
            if (r == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(
                nameof(GetEntityDetails),
                new
                {
                    EUID = r.Euid,
                    DUID = r.Duid
                },
                new
                {
                    DUID = r.Duid,
                    EUID = r.Euid,
                    Name = r.Name,
                    Description = r.Description,
                    Cost = r.Cost,
                    RemainderDate = r.RemainderDate
                }
                );
        }
        [HttpGet("IncomeAndExpenses/{FUID}")]
        public ActionResult<IEnumerable<IncomeAndExpeneses>> GetIncomeAndExpenses(Guid FUID)
        {
            var r = _repositroy.GetIncomeAndExpenses(FUID);
            if (r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpGet("Incomes/{FUID}")]
        public ActionResult<IEnumerable<IncomeAndExpeneses>> GetIncomes(Guid FUID)
        {
            var r = _repositroy.GetIncomes(FUID);
            if (r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpGet("Expenses/{FUID}")]
        public ActionResult<IEnumerable<IncomeAndExpeneses>> GetExpenses(Guid FUID)
        {
            var r = _repositroy.GetExpenses(FUID);
            if (r.Count() == 0)
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
            return CreatedAtAction(
                nameof(GetIncomeAndExpenses),
                new
                {
                    FUID = r.Fuid,
                    IEUID = r.Ieuid
                },
                new
                {
                    IEUID = r.Ieuid,
                    FUID = r.Fuid,
                    Date = r.Date,
                    Head = r.Head,
                    Description = r.Description,
                    Cost = r.Cost,
                    IncomeFlag = r.IncomeFlag
                }
                );
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
            return CreatedAtAction(
                nameof(GetIncomeAndExpenses),
                new
                {
                    FUID = r.Fuid,
                    IEUID = r.Ieuid
                },
                new
                {
                    IEUID = r.Ieuid,
                    FUID = r.Fuid,
                    Date = r.Date,
                    Head = r.Head,
                    Description = r.Description,
                    Cost = r.Cost,
                    IncomeFlag = r.IncomeFlag
                }
                );
        }
        [HttpGet("SubCategories/{CUID}")]
        public ActionResult<IEnumerable<Categories>> GetSubCategories(int CUID)
        {
            var r = _repositroy.GetSubCategoies(CUID);
            if (r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpGet("CategoryProperties/{CUID}")]
        public ActionResult<IEnumerable<CategoryOfProperties>> GetCategoryProperties(int CUID)
        {
            var r = _repositroy.GetCategoryProperties(CUID);
            if (r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
        [HttpGet("COPValues/{PUID}")]
        public ActionResult<IEnumerable<CategoryOfProperties>> GetCOPValues(int PUID)
        {
            var r = _repositroy.GetCOPValues(PUID);
            if (r.Count() == 0)
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
    }
}
