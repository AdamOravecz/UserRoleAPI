using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserRoleAPI.Models;
using UserRoleAPI.Models.Dtos;

namespace UserRoleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly UserRoleDbContext _context;
        public RoleController(UserRoleDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult> AddNewRole(AddRoleDto addRoleDto)
        {
            try
            {
                var role = new Role
                {
                    Id = Guid.NewGuid(),
                    RoleName = addRoleDto.RoleName
                };
                if (role != null)
                {
                    await _context.roles.AddAsync(role);
                    await _context.SaveChangesAsync();
                    return StatusCode(201, new { Message = "Role added successfully", result = role });
                }
                return StatusCode(400, new { Message = "Invalid role data" });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetAllRole()
        {
            try
            {
                return Ok(new { message = "Sikeres lekérdezés", result = _context.roles.ToList() });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message });
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteRole(Guid id)
        {
            try
            {
                var role = _context.roles.FirstOrDefault(u => u.Id == id);
                if (role != null)
                {
                    _context.Remove(role);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Role deleted successfully" });
                }
                return NotFound(new { message = "Role not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message });
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateRole(Guid id, UpdateRoleDto updateRoleDto)
        {
            try
            {
                var role = await _context.roles.FirstOrDefaultAsync(u => u.Id == id);
                if (role != null)
                {
                    role.RoleName = updateRoleDto.RoleName;
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Role updated successfully", result = role });
                }
                return NotFound(new { message = "Role not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message });
            }
        }
    }

}
