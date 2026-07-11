using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductionLineService.Data;
using ProductionLineService.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductionLineService.Services
{
	public class LoginService : ILoginService
	{
		private readonly AppDbContext _context;
		private readonly IConfiguration _configuration;

		public LoginService(AppDbContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}

		public async Task<string> LoginAsync(string username, string password)
		{
			var user = await _context.Users
				.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

			if (user == null)
			{
				throw new UnauthorizedAccessException("Invalid username or password.");
			}

			return GenerateJwtToken(user);
		}

		private string GenerateJwtToken(User user)
		{
			var jwtSettings = _configuration.GetSection("JwtSettings");
			var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.Username)
			};

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddHours(2),
				Issuer = jwtSettings["Issuer"],
				Audience = jwtSettings["Audience"],
				SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature)
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}