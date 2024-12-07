using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PartyApi.DTOs;
using PartyApi.Models;
using PartyApi.Repository.IRepository;

namespace PartyApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MemberController(
    ILogger<MemberController> logger,
    IUnitOfWork unitOfWork,
    IMapper mapper) : ControllerBase
{
    private readonly ILogger<MemberController> _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> GetMembers() 
    {
        var members = await _unitOfWork.MemberRepository.GetAllAsync();

        if (members == null)
        {
            return NotFound();
        }

        var membersDto = _mapper.Map<List<MemberDTO>>(members);

        return Ok(membersDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMember(int id)
    {
        var member = await _unitOfWork.MemberRepository.GetByIdWithPartyAsync(id);

        if (member == null)
        {
            return NotFound();
        }

        var memberDto = _mapper.Map<MemberDTO>(member);

        return Ok(memberDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMember(MemberNoIdDTO memberNoIdDto)
    {
        if (ModelState.IsValid)
        {
            var member = _mapper.Map<Member>(memberNoIdDto);

            await _unitOfWork.MemberRepository.CreateAsync(member);
            
            return CreatedAtAction(nameof(GetMember), new { id = member.MemberId }, member);
        }

        return new JsonResult("Something went wrong") { StatusCode = 500 };
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMember(int id, MemberDTO memberDto)
    {
        if (id != memberDto.MemberId)
        {
            return BadRequest();
        }

        var member = await _unitOfWork.MemberRepository.GetByIdAsync(id);

        if (member == null)
        {
            return NotFound();
        }

        _mapper.Map(memberDto, member);

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