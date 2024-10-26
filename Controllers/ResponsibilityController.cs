using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyApi.Data;
using PartyApi.Models;

namespace PartyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsibilityController : ControllerBase
    {
        private readonly PartyContext _context;

        public ResponsibilityController(PartyContext context)
        {
            _context = context;
        }

        // GET: api/Responsibility
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Responsibility>>> GetResponsibilities()
        {
            return await _context.Responsibilities.ToListAsync();
        }

        // GET: api/Responsibility/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Responsibility>> GetResponsibility(int id)
        {
            var responsibility = await _context.Responsibilities.FindAsync(id);

            if (responsibility == null)
            {
                return NotFound();
            }

            return responsibility;
        }

        // PUT: api/Responsibility/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResponsibility(int id, Responsibility responsibility)
        {
            if (id != responsibility.ResponsibilityId)
            {
                return BadRequest();
            }

            _context.Entry(responsibility).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResponsibilityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Responsibility
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Responsibility>> PostResponsibility(Responsibility responsibility)
        {
            _context.Responsibilities.Add(responsibility);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResponsibility", new { id = responsibility.ResponsibilityId }, responsibility);
        }

        // DELETE: api/Responsibility/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResponsibility(int id)
        {
            var responsibility = await _context.Responsibilities.FindAsync(id);
            if (responsibility == null)
            {
                return NotFound();
            }

            _context.Responsibilities.Remove(responsibility);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResponsibilityExists(int id)
        {
            return _context.Responsibilities.Any(e => e.ResponsibilityId == id);
        }
    }
}
