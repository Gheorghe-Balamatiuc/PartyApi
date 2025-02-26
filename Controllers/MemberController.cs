using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize("read:data")]
    public async Task<IActionResult> GetMembers() 
    {
        var members = await _unitOfWork.MemberRepository.GetAllAsync();

        if (members == null)
        {
            _logger.LogWarning("Members not found");
            return NotFound();
        }

        var membersDto = _mapper.Map<List<MemberDTO>>(members);

        return Ok(membersDto);
    }

    [HttpGet("{id}")]
    [Authorize("read:data")]
    public async Task<IActionResult> GetMember(int id)
    {
        var member = await _unitOfWork.MemberRepository.GetByIdWithPartyAsync(id);

        if (member == null)
        {
            _logger.LogWarning("Member with id {id} not found", id);
            return NotFound();
        }

        var memberDto = _mapper.Map<MemberDTO>(member);

        return Ok(memberDto);
    }

    [HttpPost]
    [Authorize("modify:data")]
    public async Task<IActionResult> CreateMember(MemberNoIdDTO memberNoIdDto)
    {
        if (ModelState.IsValid)
        {
            var member = _mapper.Map<Member>(memberNoIdDto);

            await _unitOfWork.MemberRepository.CreateAsync(member);
            
            return CreatedAtAction(nameof(GetMember), new { id = member.MemberId }, member);
        }

        _logger.LogWarning("Member model state is invalid");

        return new JsonResult("Something went wrong") { StatusCode = 500 };
    }

    [HttpPut("{id}")]
    [Authorize("modify:data")]
    public async Task<IActionResult> UpdateMember(int id, MemberDTO memberDto)
    {
        if (id != memberDto.MemberId)
        {
            _logger.LogWarning("Member id {id} does not match member id {memberDto.MemberId}", id, memberDto.MemberId);
            return BadRequest();
        }

        var member = await _unitOfWork.MemberRepository.GetByIdAsync(id);

        if (member == null)
        {
            _logger.LogWarning("Member with id {id} not found", id);
            return NotFound();
        }

        _mapper.Map(memberDto, member);

        await _unitOfWork.MemberRepository.UpdateAsync(member);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize("modify:data")]
    public async Task<IActionResult> DeleteMember(int id)
    {
        var member = await _unitOfWork.MemberRepository.GetByIdAsync(id);

        if (member == null)
        {
            _logger.LogWarning("Member with id {id} not found", id);
            return NotFound();
        }

        await _unitOfWork.MemberRepository.DeleteByIdAsync(id);

        return NoContent();
    }
}