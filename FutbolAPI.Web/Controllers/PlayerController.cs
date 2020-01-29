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
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerAPI PlayerAPI;

        public PlayerController(IPlayerAPI PlayerAPI)
        {
            this.PlayerAPI = PlayerAPI;
        }
        [HttpGet]
        [Route("api/Player/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                return this.Ok(await PlayerAPI.GetById(Id));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("api/Player")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return this.Ok(await PlayerAPI.GetAll());
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("api/Player/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody]PlayerVM PlayerVM)
        {
            try
            {
                Player m = await this.PlayerAPI.GetById(Id);
                m = PlayerVM.ToModel(m);             
                return this.Ok(await PlayerAPI.Update(m));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("api/Player")]
        public async Task<IActionResult> Create([FromBody]PlayerVM PlayerVM)
        {
            try
            {
                Player m = PlayerVM.ToModel();
                return this.Ok(await PlayerAPI.Add(m));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("api/Player/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                return this.Ok(await PlayerAPI.Delete(Id));
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}