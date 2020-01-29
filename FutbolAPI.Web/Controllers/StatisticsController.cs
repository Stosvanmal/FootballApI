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
    public class StatisticsController : ControllerBase
    {
        private readonly IPlayerAPI playerAPI;

        public StatisticsController(IPlayerAPI playerAPI)
        {
            this.playerAPI = playerAPI;
        }
        [HttpGet]
        [Route("api/Statistics/yellowcard")]
        public async Task<IActionResult> YellowCard()
        {
            try
            {
                var yList = await playerAPI.GetYellowCardPlayers();
                return this.Ok(yList.ToList().ToViewModelLst(false));
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }
        [HttpGet]
        [Route("api/Statistics/redcard")]
        public async Task<IActionResult> Redcard()
        {
            try
            {
                var rList = await playerAPI.GetYellowCardPlayers();
                return this.Ok(rList.ToList().ToViewModelLst(true));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        [HttpGet]
        [Route("api/Statistics/minutes")]
        public async Task<IActionResult> Minutes()
        {
            try
            {
                var mList = await playerAPI.GetMinutesPlayers();
                return this.Ok(mList.ToList().ToViewModelLst());
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

    }
}