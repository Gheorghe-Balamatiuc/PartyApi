using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyApi.DTOs;
using PartyApi.Models;
using PartyApi.Repository.IRepository;

namespace PartyApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(
    ILogger<UserController> logger,
    IUnitOfWork unitOfWork,
    IMapper mapper) : ControllerBase
{
    private readonly ILogger<UserController> _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    [Authorize("read:data")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();

        if (users == null)
        {
            _logger.LogWarning("Users not found");
            return NotFound();
        }

        var usersDto = _mapper.Map<IEnumerable<UserDTO>>(users);

        return Ok(usersDto);
    }

    [HttpGet("{id}")]
    [Authorize("read:data")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _unitOfWork.UserRepository.GetUserWithPartiesAsync(id);

        if (user == null)
        {
            _logger.LogWarning("User with id {id} not found", id);
            return NotFound();
        }

        var userWithPartiesDTO = _mapper.Map<UserWithPartiesDTO>(user);

        return Ok(userWithPartiesDTO);
    }

    [HttpPost]
    [Authorize("modify:data")]
    public async Task<IActionResult> CreateUser(UserNoIdDTO userNoIdDTO)
    {
        if (ModelState.IsValid)
        {
            var user = _mapper.Map<User>(userNoIdDTO);

            await _unitOfWork.UserRepository.CreateAsync(user);

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }
        
        _logger.LogWarning("User model state is invalid");

        return new JsonResult("Something went wrong") { StatusCode = 500 };
    }

    [HttpPut("{id}")]
    [Authorize("modify:data")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        if (id != user.UserId)
        {
            _logger.LogWarning("User id {id} does not match user id {user.UserId}", id, user.UserId);
            return BadRequest();
        }

        await _unitOfWork.UserRepository.UpdateAsync(user);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize("modify:data")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

        if (user == null)
        {
            _logger.LogWarning("User with id {id} not found", id);
            return NotFound();
        }

        await _unitOfWork.UserRepository.DeleteByIdAsync(id);

        return NoContent();
    }
}