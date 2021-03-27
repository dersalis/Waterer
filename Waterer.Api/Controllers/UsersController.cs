using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using AuthenticationPlugin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Waterer.Api.Data;
using Waterer.Api.Models;
using Waterer.Api.ViewModels;

namespace Waterer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly WatererDBContext _context;
        private IConfiguration _configuration;
        private readonly AuthService _auth;

        public UsersController(WatererDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _auth = new AuthService(_configuration);
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            // Sprawdz czy uzytkownik istnieje w bazie
            var userFromDB = _context.Users.SingleOrDefault(u => u.Email == model.Email);
            // Jeśli juz istnieje
            if (userFromDB != null) return BadRequest($"Uzytkonwnik {model.Email} juz istnieje.");
            
            // Jeśli nie istnieje to utworz
            var newUser = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = SecurePasswordHasherHelper.Hash(model.Password),
                Role = model.Role
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            // Sprawdz czy uzytkownik istnieje
            var userFromDB = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            // Jeśli uzytkownik nie istnieje
            if(userFromDB == null) return NotFound("Nie można odnaleźć konta z taką nazwą użytkownika.");

            // Sprawdz hasło
            if (!SecurePasswordHasherHelper.Verify(model.Password, userFromDB.Password))
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, userFromDB.Email),
                new Claim(ClaimTypes.Email, userFromDB.Email),
                new Claim(ClaimTypes.Role, userFromDB.Role),
            };
            var token = _auth.GenerateAccessToken(claims);

            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                expires_in = token.ExpiresIn,
                token_type = token.TokenType,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                user_id = userFromDB.Id,
            });
        }

        [Authorize]
        [HttpPut("changepassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            // Sprawdz czy uzytkownik istnieje
            var userFromDB = _context.Users.FirstOrDefault(u => u.Email == model.Email);

            // Jeśli uzytkownik nie istnieje lub stare hasło jest nie poprawne
            if(userFromDB == null || !SecurePasswordHasherHelper.Verify(model.OldPassword, userFromDB.Password)) return NotFound("Nie można odnaleźć konta z taką nazwą użytkownika.");

            // Zmień hasło
            userFromDB.Password = SecurePasswordHasherHelper.Hash(model.NewPassword);

            _context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}