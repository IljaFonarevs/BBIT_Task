using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uzd2.Datatypes;
using Uzd2.DTOs;
using Uzd2.Services;

namespace Uzd2.Controllers
{
    [Route("api/Iedz")]
    [ApiController]
    public class IedzivotajsController : ControllerBase
    {
        private readonly IIedzService _iedzService;

        public IedzivotajsController(IIedzService iedzService)
        {
            _iedzService = iedzService;
        }

        // GET: api/Iedzivotajs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Iedzivotajs>>> GetIedzivotajsItems()
        {
            return await _iedzService.GetIedz();
        }

        // GET: api/Iedzivotajs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Iedzivotajs>> GetIedzivotajs(long id)
        {
            var Iedz = await _iedzService.GetIedzById(id);

            if (Iedz == null) return NotFound();

            return Ok(_iedzService.GetIedzDTO(Iedz));
        }

        // PUT: api/Iedzivotajs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIedzivotajs(long id, IedzDTO iedzDTO)
        {
            var Iedz = _iedzService.GetIedzFromDTO(iedzDTO);

            if (id != Iedz.PersKods)
            {
                return BadRequest();
            }

            var iedz = _iedzService.UpdateIedz(id, Iedz);
            if(iedz == null) return NotFound();

            return NoContent();
        }

        // POST: api/Iedzivotajs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Iedzivotajs>> PostIedzivotajs(IedzDTO iedzDTO)
        {
            var Iedz = _iedzService.GetIedzFromDTO(iedzDTO);

            var iedzivotajs = await _iedzService.CreateIedz(Iedz);
            if (iedzivotajs == null) return Conflict();

            return CreatedAtAction("GetIedzivotajs", new { id = Iedz.PersKods }, Iedz);
        }

        // DELETE: api/Iedzivotajs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIedzivotajs(long id)
        {
            var Iedz = await _iedzService.DeleteIedz(id);
            if (Iedz == null)return NotFound();
           
            return NoContent();
        }
    }
}
