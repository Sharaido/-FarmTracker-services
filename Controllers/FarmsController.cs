﻿using System;
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
    }
}
