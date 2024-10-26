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
    public class MemberResponsibilityController : ControllerBase
    {
        private readonly PartyContext _context;

        public MemberResponsibilityController(PartyContext context)
        {
            _context = context;
        }

        // GET: api/MemberResponsibility
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberResponsibility>>> GetMemberResponsibilities()
        {
            return await _context.MemberResponsibilities.ToListAsync();
        }

        // GET: api/MemberResponsibility/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MemberResponsibility>> GetMemberResponsibility(int id)
        {
            var memberResponsibility = await _context.MemberResponsibilities.FindAsync(id);

            if (memberResponsibility == null)
            {
                return NotFound();
            }

            return memberResponsibility;
        }

        // PUT: api/MemberResponsibility/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMemberResponsibility(int id, MemberResponsibility memberResponsibility)
        {
            if (id != memberResponsibility.MemberResponsibilityId)
            {
                return BadRequest();
            }

            _context.Entry(memberResponsibility).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberResponsibilityExists(id))
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

        // POST: api/MemberResponsibility
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MemberResponsibility>> PostMemberResponsibility(MemberResponsibility memberResponsibility)
        {
            _context.MemberResponsibilities.Add(memberResponsibility);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMemberResponsibility", new { id = memberResponsibility.MemberResponsibilityId }, memberResponsibility);
        }

        // DELETE: api/MemberResponsibility/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMemberResponsibility(int id)
        {
            var memberResponsibility = await _context.MemberResponsibilities.FindAsync(id);
            if (memberResponsibility == null)
            {
                return NotFound();
            }

            _context.MemberResponsibilities.Remove(memberResponsibility);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemberResponsibilityExists(int id)
        {
            return _context.MemberResponsibilities.Any(e => e.MemberResponsibilityId == id);
        }
    }
}
