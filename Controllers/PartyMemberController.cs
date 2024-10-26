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
    public class PartyMemberController : ControllerBase
    {
        private readonly PartyContext _context;

        public PartyMemberController(PartyContext context)
        {
            _context = context;
        }

        // GET: api/PartyMember
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartyMember>>> GetPartyMembers()
        {
            return await _context.PartyMembers.ToListAsync();
        }

        // GET: api/PartyMember/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PartyMember>> GetPartyMember(int id)
        {
            var partyMember = await _context.PartyMembers.FindAsync(id);

            if (partyMember == null)
            {
                return NotFound();
            }

            return partyMember;
        }

        // PUT: api/PartyMember/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartyMember(int id, PartyMember partyMember)
        {
            if (id != partyMember.PartyMemberId)
            {
                return BadRequest();
            }

            _context.Entry(partyMember).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartyMemberExists(id))
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

        // POST: api/PartyMember
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PartyMember>> PostPartyMember(PartyMember partyMember)
        {
            _context.PartyMembers.Add(partyMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPartyMember", new { id = partyMember.PartyMemberId }, partyMember);
        }

        // DELETE: api/PartyMember/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartyMember(int id)
        {
            var partyMember = await _context.PartyMembers.FindAsync(id);
            if (partyMember == null)
            {
                return NotFound();
            }

            _context.PartyMembers.Remove(partyMember);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PartyMemberExists(int id)
        {
            return _context.PartyMembers.Any(e => e.PartyMemberId == id);
        }
    }
}
