using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uzd2.Datatypes;
using Uzd2.DTOs;
using Uzd2.Services;


namespace Uzd2.Controllers
{
    [Route("api/Dzivoklis")]
    [ApiController]
    public class DzivoklisController : ControllerBase
    {
        private readonly IDzivService _dzivService;

        public DzivoklisController(IDzivService dzivService)
        {
            _dzivService = dzivService;
        }

        // GET: api/Dzivoklis
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<IEnumerable<Dzivoklis>>> GetDzivoklisItems()
        {
            return await _dzivService.GetDziv();
        }

        [HttpGet("{houseID}/apartments")]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Dzivoklis>>> GetApartmentsFromHouse(int houseID)
        {
            return await _dzivService.GetApartmentsFromHouse(houseID);
        }

        // GET: api/Dzivoklis/5
        [HttpGet("{id}")]
       //Authorize(Policy = "CorrectApartment")]
        [AllowAnonymous]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<Dzivoklis>> GetDzivoklis(long id)
        {
            var dziv = await _dzivService.GetDzivById(id);

            if (dziv == null) return NotFound();
            

            return Ok(_dzivService.GetDzivDTO(dziv));
        }

        // PUT: api/Dzivoklis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> PutDzivoklis(long id, DzivDTO dzivDTO)
        {
            var dzivoklis = _dzivService.GetDzivFromDTO(dzivDTO);
            if (id != dzivoklis.DzivNumurs)
            {
                return BadRequest();
            }


            var dziv = await _dzivService.UpdateDziv(id, dzivoklis);
            if (dziv == null) return NotFound();

            return NoContent();
        }

        // POST: api/Dzivoklis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult<Dzivoklis>> PostDzivoklis(DzivDTO dzivoklis)
        {
            var dziv = _dzivService.GetDzivFromDTO(dzivoklis);
            dziv = await _dzivService.CreateDziv(dziv);
            if(dziv == null) return Conflict();

            return CreatedAtAction("GetDzivoklis", new { id = dzivoklis.DzivNumurs }, dzivoklis);
        }

        // DELETE: api/Dzivoklis/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteDzivoklis(long id)
        {
            var dzivoklis = await _dzivService.DeleteDziv(id);
            if (dzivoklis == null) return NotFound();

            return NoContent();
        }

    }
}
