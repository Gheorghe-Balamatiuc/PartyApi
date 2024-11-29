using Microsoft.AspNetCore.Mvc;
using PartyApi.Models;
using PartyApi.Repository.IRepository;

namespace PartyApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PartyController : ControllerBase
{
    private readonly ILogger<PartyController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public PartyController(ILogger<PartyController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetParties() 
    {
        var parties = await _unitOfWork.PartyRepository.GetAllAsync();

        if (parties == null)
        {
            return NotFound();
        }

        return Ok(parties);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetParty(int id)
    {
        var party = await _unitOfWork.PartyRepository.GetByIdAsync(id);

        if (party == null)
        {
            return NotFound();
        }

        return Ok(party);
    }

    [HttpPost]
    public async Task<IActionResult> CreateParty(Party party)
    {
        if (ModelState.IsValid)
        {
            await _unitOfWork.PartyRepository.CreateAsync(party);
            
            return CreatedAtAction(nameof(GetParty), new { id = party.PartyId }, party);
        }

        return new JsonResult("Something went wrong") { StatusCode = 500 };
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateParty(int id, Party party)
    {
        if (id != party.PartyId)
        {
            return BadRequest();
        }

        await _unitOfWork.PartyRepository.UpdateAsync(party);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParty(int id)
    {
        var party = await _unitOfWork.PartyRepository.GetByIdAsync(id);

        if (party == null)
        {
            return NotFound();
        }

        await _unitOfWork.PartyRepository.DeleteByIdAsync(id);

        return NoContent();
    }
}