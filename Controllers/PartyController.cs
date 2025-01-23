using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyApi.DTOs;
using PartyApi.Models;
using PartyApi.Repository.IRepository;

namespace PartyApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PartyController(
    ILogger<PartyController> logger, 
    IUnitOfWork unitOfWork,
    IMapper mapper) : ControllerBase
{
    private readonly ILogger<PartyController> _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    [Authorize("read:data")]
    public async Task<IActionResult> GetParties() 
    {
        var parties = await _unitOfWork.PartyRepository.GetAllAsync();

        if (parties == null)
        {
            return NotFound();
        }

        var partiesDto = _mapper.Map<IEnumerable<PartyDTO>>(parties);

        return Ok(partiesDto);
    }

    [HttpGet("{id}")]
    [Authorize("read:data")]
    public async Task<IActionResult> GetParty(int id)
    {
        var party = await _unitOfWork.PartyRepository.GetPartyWithMembersAsync(id);

        if (party == null)
        {
            return NotFound();
        }

        var partyWithMembersDto = _mapper.Map<PartyWithMembersDTO>(party);

        return Ok(partyWithMembersDto);
    }

    [HttpPost]
    [Authorize("modify:data")]
    public async Task<IActionResult> CreateParty(PartyNoIdDTO partyNoIdDto)
    {
        if (ModelState.IsValid)
        {
            var party = _mapper.Map<Party>(partyNoIdDto);

            await _unitOfWork.PartyRepository.CreateAsync(party);
            
            return CreatedAtAction(nameof(GetParty), new { id = party.PartyId }, party);
        }

        return new JsonResult("Something went wrong") { StatusCode = 500 };
    }

    [HttpPut("{id}")]
    [Authorize("modify:data")]
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
    [Authorize("modify:data")]
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