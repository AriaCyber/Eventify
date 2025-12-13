using Eventify.Data;
using Eventify.Dtos;
using Eventify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;
    public UserController(AppDbContext context)
    {
        _context = context;
    }

    //GET api/user
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        //DTO so password is not exposed
        var users = await _context.Users
            .Select(u => new UserDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email
            })
            .ToListAsync();
        return Ok(users);
    }

    //POST api/user/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
            return BadRequest(new { message = "Email and password are required" });
        var exists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
        if (exists)
            return BadRequest(new { message = "Email already exists" });
        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Password = dto.Password
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        var result = new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email
        };

        return Ok(result);
    }

    //POST api/user/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
            return BadRequest(new { message = "Email and password are required" });
        
        //hardcoded admin login
        if (dto.Email == "admin@eventify.com" && dto.Password == "admin123")
        {
            return Ok(new
            {
                message = "Admin login success",
                isAdmin = true
            });
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null || user.Password != dto.Password)
            return BadRequest(new { message = "Invalid email or password" });
        var result = new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email
        };
        return Ok(result);
    }
}
