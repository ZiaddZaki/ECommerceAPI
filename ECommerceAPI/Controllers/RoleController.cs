using ECommerceAPI.BLL.DTOs.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]

    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<ActionResult> CreateRole([FromBody] RoleCreateDto roleCreateDto)
        {
            IdentityRole applicationRole = new IdentityRole
            {
                Name = roleCreateDto.Name
            };
            var result = await _roleManager.CreateAsync(applicationRole);
            return Ok(result);
        }
    }
}
