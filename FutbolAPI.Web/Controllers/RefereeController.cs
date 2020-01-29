using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FutbolAPI.Business.API;
using FutbolAPI.Business.Models;
using FutbolAPI.Web.Mappers;
using FutbolAPI.Web.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FutbolAPI.Web.Controllers
{
    
    [ApiController]
    public class RefereeController : ControllerBase
    {
        private readonly IRefereeAPI RefereeAPI;

        public RefereeController(IRefereeAPI RefereeAPI)
        {
            this.RefereeAPI = RefereeAPI;
        }
        [HttpGet]
        [Route("api/Referee/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                return this.Ok(await RefereeAPI.GetById(Id));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("api/Referee")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return this.Ok(await RefereeAPI.GetAll());
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("api/Referee/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody]RefereeVM RefereeVM)
        {
            try
            {
                Referee m = await this.RefereeAPI.GetById(Id);
                m = RefereeVM.ToModel(m);             
                return this.Ok(await RefereeAPI.Update(m));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("api/Referee")]
        public async Task<IActionResult> Create([FromBody]RefereeVM RefereeVM)
        {
            try
            {
                Referee m = RefereeVM.ToModel();
                return this.Ok(await RefereeAPI.Add(m));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("api/Referee/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                return this.Ok(await RefereeAPI.Delete(Id));
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}