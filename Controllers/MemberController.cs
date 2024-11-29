using Microsoft.AspNetCore.Mvc;
using PartyApi.Models;
using PartyApi.Repository.IRepository;

namespace PartyApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MemberController : ControllerBase
{
    private readonly ILogger<MemberController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public MemberController(ILogger<MemberController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetMembers() 
    {
        var members = await _unitOfWork.MemberRepository.GetAllAsync();

        if (members == null)
        {
            return NotFound();
        }

        return Ok(members);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMember(int id)
    {
        var member = await _unitOfWork.MemberRepository.GetByIdAsync(id);

        if (member == null)
        {
            return NotFound();
        }

        return Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMember(Member member)
    {
        if (ModelState.IsValid)
        {
            await _unitOfWork.MemberRepository.CreateAsync(member);
            
            return CreatedAtAction(nameof(GetMember), new { id = member.MemberId }, member);
        }

        return new JsonResult("Something went wrong") { StatusCode = 500 };
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMember(int id, Member member)
    {
        if (id != member.MemberId)
        {
            return BadRequest();
        }

        await _unitOfWork.MemberRepository.UpdateAsync(member);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMember(int id)
    {
        var member = await _unitOfWork.MemberRepository.GetByIdAsync(id);

        if (member == null)
        {
            return NotFound();
        }

        await _unitOfWork.MemberRepository.DeleteByIdAsync(id);

        return NoContent();
    }
}