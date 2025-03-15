using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyApi.Repository.IRepository;

namespace PartyApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PartyUserController(
    IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpPost("{userId}/{partyId}")]
    [Authorize("modify:data")]
    public async Task<IActionResult> AddPartyToUser(int userId, int partyId)
    {
        var success = await _unitOfWork.UserRepository.AddPartyToUserAsync(userId, partyId);

        if (!success)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{userId}/{partyId}")]
    [Authorize("modify:data")]
    public async Task<IActionResult> RemovePartyFromUser(int userId, int partyId)
    {
        var success = await _unitOfWork.UserRepository.RemovePartyFromUserAsync(userId, partyId);

        if (!success)
        {
            return BadRequest();
        }

        return NoContent();
    }
}