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
    public class ManagerController : ControllerBase
    {
        private readonly IManagerAPI managerAPI;

        public ManagerController(IManagerAPI managerAPI)
        {
            this.managerAPI = managerAPI;
        }
        [HttpGet]
        [Route("api/Manager/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                return this.Ok(await managerAPI.GetById(Id));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("api/Manager")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return this.Ok(await managerAPI.GetAll());
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("api/Manager/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody]ManagerVM managerVM)
        {
            try
            {
                Manager m = await this.managerAPI.GetById(Id);
                m = managerVM.ToModel(m);             
                return this.Ok(await managerAPI.Update(m));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("api/Manager")]
        public async Task<IActionResult> Create([FromBody]ManagerVM managerVM)
        {
            try
            {
                Manager m = managerVM.ToModel();
                return this.Ok(await managerAPI.Add(m));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("api/Manager/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                return this.Ok(await managerAPI.Delete(Id));
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}