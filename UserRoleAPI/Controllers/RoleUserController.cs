using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRoleAPI.Models;
using UserRoleAPI.Models.Dtos;

namespace UserRoleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleUserController : ControllerBase
    {
        private readonly UserRoleDbContext _context;
        public RoleUserController(UserRoleDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult> AddNewRoleToUser(AddNewSwitchDto roleUser)
        { 
            try
            {
                var roleuser = new RoleUser
                {
                    UserId = roleUser.UserId,
                    RoleId = roleUser.RoleId
                };
                await _context.roleusers.AddAsync(roleuser);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Role assigned to user successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message });
            }
        }
    }
}
