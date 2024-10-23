using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uzd2.Datatypes;
using Uzd2.DTOs;
using Uzd2.Services;

namespace Uzd2.Controllers
{
    [Route("api/Majas")]
    [ApiController]
    public class MajasController : ControllerBase
    {
        private readonly IMajaService _majaService;


        public MajasController(IMajaService majaService)
        {
            _majaService = majaService;
        }

        // GET: api/Majas
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Maja>>> GetMajaItems()
        {
            return await _majaService.GetMaja();
        }

        // GET: api/Majas/5
        [HttpGet("{id}")]
        [Authorize(Policy = "CorrectApartmentHouse")]
        public async Task<ActionResult<MajaDTO>> GetMaja(long id)
        {
            var maja = await _majaService.GetMajaById(id);

            if (maja == null)return NotFound();

            return Ok(_majaService.GetMajaDTO(maja));
        }

        // PUT: api/Majas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutMaja(long id, MajaDTO majaDTO)
        {
            var maja = _majaService.GetMajaFromDTO(majaDTO);
            if (id != maja.MajaNumurs)
            {
                return BadRequest();
            }

            
            maja = await _majaService.UpdateMaja(id, maja);
            if (maja == null) return NotFound();

            
            return NoContent();
        }

        // POST: api/Majas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Maja>> PostMaja(MajaDTO majaDTO)
        {
            var maja = _majaService.GetMajaFromDTO(majaDTO);
            maja = await _majaService.CreateMaja(maja);

            return CreatedAtAction("GetMaja", new { id = maja.MajaNumurs }, maja);
        }

        // DELETE: api/Majas/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMaja(long id)
        {
            var maja = await _majaService.DeleteMaja(id);
            if (maja == null)return NotFound();
           
            return NoContent();
        }

        
    }
}
