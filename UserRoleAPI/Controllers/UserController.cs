using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRoleAPI.Models;
using UserRoleAPI.Models.Dtos;

namespace UserRoleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRoleDbContext _context;
        public UserController(UserRoleDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult> AddNewUser(AddUserDto addUserDto)
        {
            try
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = addUserDto.Name,
                    Email = addUserDto.Email,
                    Password = addUserDto.Password
                };
                if (user != null)
                {
                    await _context.users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return StatusCode(201, new { Message = "User added successfully", result = user });
                }
                return StatusCode(400, new { Message = "Invalid user data" });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            try
            {
                return Ok(new {message = "Sikeres lekérdezés", result = _context.users.ToList()});
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message });
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            try
            {
                var user = _context.users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    _context.Remove(user);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "User deleted successfully" });
                }
                return NotFound(new { message = "User not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message });
            }
        }

    }
}
