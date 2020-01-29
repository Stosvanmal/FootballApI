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
    public class MatchController : ControllerBase
    {
        private readonly IMatchAPI MatchAPI;

        public MatchController(IMatchAPI MatchAPI)
        {
            this.MatchAPI = MatchAPI;
        }
        [HttpGet]
        [Route("api/Match/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                return this.Ok((await MatchAPI.GetById(Id)).ToViewModel());
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("api/Match")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return this.Ok((await MatchAPI.GetAll()).ToViewModelLst());
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("api/Match/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody]MatchVMRequest MatchVM)
        {
            try
            {
                Match m = await this.MatchAPI.GetById(Id);
                m = MatchVM.ToModel(m);
                return this.Ok(await MatchAPI.Update(m));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("api/Match")]
        public async Task<IActionResult> Create([FromBody]MatchVMRequest MatchVM)
        {
            try
            {
                Match m = MatchVM.ToModel();
                return this.Ok(await MatchAPI.Add(m));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("api/Match/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                return this.Ok(await MatchAPI.Delete(Id));
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}