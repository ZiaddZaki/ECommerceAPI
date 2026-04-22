using ECommerceAPI.BLL.DTOs.Auth;
using ECommerceAPI.DAL;
using ECommerceAPI.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtSettings, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDto UserLogin)
        {
            var user = await _userManager.FindByEmailAsync(UserLogin.Email);
            if (user == null)
            {
                return Unauthorized("invalid email or password");
            }
            var result = await _userManager.CheckPasswordAsync(user, UserLogin.Password);

            if (!result)
            {
                return Unauthorized("invalid email or password");
            }
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName!));
            claims.Add(new Claim(ClaimTypes.Email, user.Email!));

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDto = GenerateToken(claims);
            return Ok(tokenDto);

        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDTo registerDTo)
        {
            var applicationUser = new ApplicationUser
            {
                FirstName = registerDTo.FirstName,
                LastName = registerDTo.LastName,
                Email = registerDTo.Email,
                UserName = registerDTo.UserName,
            };
            var result = await _userManager.CreateAsync(applicationUser, registerDTo.Password);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            var roleresult = await _userManager.AddToRoleAsync(applicationUser, "Admin");

            var cart = new Cart
            {
                UserId = applicationUser.Id,
            };
            _unitOfWork.CartRepository.Add(cart);
            await _unitOfWork.SaveChangesAsync();
            return Ok("User Registerd Successfully ✅");
        }

        public TokenDTo GenerateToken(List<Claim> claims)
        {
            var keyConfig = _jwtSettings.SecretKey;
            var keyInBytes = Convert.FromBase64String(keyConfig);
            var key = new SymmetricSecurityKey(keyInBytes);
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiryDate = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes);

            var jwt = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                signingCredentials: signingCredentials,
                expires: expiryDate
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            var tokenDTo = new TokenDTo(token, _jwtSettings.DurationInMinutes);

            return tokenDTo;
        }
    }
}
