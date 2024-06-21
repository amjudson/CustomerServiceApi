using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using CustomerServiceApi.Data;
using CustomerServiceApi.Models;
using CustomerServiceApi.Models.Dto;
using CustomerServiceApi.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace CustomerServiceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
	ApplicationDbContext db,
	IConfiguration config,
	UserManager<ApplicationUser> userManager,
	RoleManager<IdentityRole> roleManager) : ControllerBase
{
	private readonly ApiResponse response = new();
	private readonly string secretKey = config["AppSettings:Secret"];

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
	{
		var userFromDb = await db.ApplicationUsers
			.FirstOrDefaultAsync(u => u.UserName.ToLower() == model.Username.ToLower());
		var validPassword = await userManager.CheckPasswordAsync(userFromDb, model.Password);
		if (!validPassword)
		{
			response.Result = new LoginResponseDto();
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			response.ErrorMessages.Add("Invalid username or password");
			return BadRequest(response);
		}

		var roles = await userManager.GetRolesAsync(userFromDb);
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(secretKey);
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new []
			{
				new Claim("lastName", userFromDb.LastName),
				new Claim("firstName", userFromDb.FirstName),
				new Claim("id", userFromDb.Id),
				new Claim(ClaimTypes.Email, userFromDb.UserName),
				new Claim(ClaimTypes.Role, roles.FirstOrDefault())
			}),
			Expires = DateTime.UtcNow.AddDays(1),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);

		var loginResponse = new LoginResponseDto
		{
			Email = userFromDb.Email,
			Token = tokenHandler.WriteToken(token)
		};

		if (loginResponse.Email == null || string.IsNullOrEmpty(loginResponse.Token))
		{
			response.Result = new LoginResponseDto();
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			response.ErrorMessages.Add("Invalid username or password");
			return BadRequest(response);
		}

		response.StatusCode = HttpStatusCode.OK;
		response.Success = true;
		response.Result = loginResponse;
		return Ok(response);
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
	{
		var userFromDb = await db.ApplicationUsers
			.FirstOrDefaultAsync(u => u.UserName.ToLower() == model.Username.ToLower());
		if (userFromDb != null)
		{
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			response.ErrorMessages.Add("User already exists");
			return BadRequest(response);
		}

		var newUser = new ApplicationUser
		{
			Email = model.Username,
			UserName = model.Username,
			NormalizedEmail = model.Username.ToUpper(),
			FirstName = model.FirstName,
			LastName = model.LastName,
		};

		try
		{
			var result = await userManager.CreateAsync(newUser, model.Password);
			if (result.Succeeded)
			{
				if (!await roleManager.RoleExistsAsync(StaticData.Role_Admin))
				{
					await roleManager.CreateAsync(new IdentityRole(StaticData.Role_Admin));
				}
				if (!await roleManager.RoleExistsAsync(StaticData.Role_Client_Admin))
				{
					await roleManager.CreateAsync(new IdentityRole(StaticData.Role_Client_Admin));
				}
				if (!await roleManager.RoleExistsAsync(StaticData.Role_User))
				{
					await roleManager.CreateAsync(new IdentityRole(StaticData.Role_User));
				}

				switch (model.Role)
				{
					case StaticData.Role_Admin:
						await userManager.AddToRoleAsync(newUser, StaticData.Role_Admin);
						break;
					case StaticData.Role_Client_Admin:
						await userManager.AddToRoleAsync(newUser, StaticData.Role_Client_Admin);
						break;
					default:
						await userManager.AddToRoleAsync(newUser, StaticData.Role_User);
						break;
				}

				response.Result = new { UserId = newUser.Id };
				response.StatusCode = HttpStatusCode.OK;
				return Ok(response);
			}
		}
		catch (Exception e)
		{
			response.ErrorMessages.Add($"User creation failed for {model.Username}\n{e.Message}");
		}

		response.Success = false;
		response.StatusCode = HttpStatusCode.BadRequest;
		response.ErrorMessages.Add($"User creation failed for {model.Username}, please check the data provided");
		return BadRequest(response);
	}
}