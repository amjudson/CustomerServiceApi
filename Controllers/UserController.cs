using CustomerServiceApi.Data;
using CustomerServiceApi.Models;
using CustomerServiceApi.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace CustomerServiceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(
	ApplicationDbContext db,
	UserManager<ApplicationUser> userManager,
	RoleManager<IdentityRole> roleManager) : ControllerBase
{
	private readonly ApiResponse response = new();

	[HttpGet("GetUserList", Name = "GetUserList")]
	public async Task<ActionResult<ApiResponse>> GetUserList()
	{
		var users = await db.ApplicationUsers.ToListAsync();
		var usersDtos = new List<ApplicationUserDto>();
		foreach (var user in users)
		{
			var userDto = FormatApplicationUserDto(user);

			var userRoles = await userManager.GetRolesAsync(user);
			userDto.Roles = [];
			foreach (var role in userRoles)
			{
				userDto.Roles.Add(role);
			}

			var claims = await userManager.GetClaimsAsync(user);
			userDto.UserClaims = [];
			foreach (var claim in claims)
			{
				userDto.UserClaims.Add(claim.Type);
			}

			usersDtos.Add(userDto);
		}

		response.Result = usersDtos.OrderBy(u => u.Email);
		response.Success = true;
		response.StatusCode = HttpStatusCode.OK;
		return response;
	}

	[HttpGet("GetUserManageRole/{userId}", Name = "GetUserManageRole")]
	public async Task<ActionResult<ApiResponse>> GetUserManageRole(string userId)
	{
		var user = await userManager.FindByIdAsync(userId);
		if (user == null)
		{
			response.ErrorMessages.Add($"User '{userId}' not found");
			response.Success = false;
			response.StatusCode = HttpStatusCode.NotFound;
			return NotFound(response);
		}

		var userDto = new RolesViewModel
		{
			UserDto = FormatApplicationUserDto(user),
		};

		var existingUserRoles = await userManager.GetRolesAsync(user) as List<string>;
		foreach (var role in roleManager.Roles)
		{
			var roleSelection = new RoleSelection
			{
				RoleName = role.Name,
				Selected = existingUserRoles?.Any(r => r == role.Name) ?? false,
			};

			userDto.RolesList.Add(roleSelection);
		}

		response.Result = userDto;
		response.Success = true;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}

	[HttpPost("PostUserManageRole", Name = "PostUserManageRole")]
	public async Task<ActionResult<ApiResponse>> PostUserManageRoles([FromBody] RolesViewModel rolesViewModel)
	{
		var user = await userManager.FindByIdAsync(rolesViewModel.UserDto.Id);
		if (user == null)
		{
			response.ErrorMessages.Add($"User '{rolesViewModel.UserDto.Id}' not found");
			response.Success = false;
			response.StatusCode = HttpStatusCode.NotFound;
			return NotFound(response);
		}

		var oldUserRoles = await userManager.GetRolesAsync(user);
		var result = await userManager.RemoveFromRolesAsync(user, oldUserRoles);
		if (!result.Succeeded)
		{
			response.ErrorMessages.Add($"Failed to remove old Roles for user '{rolesViewModel.UserDto.Id}'.");
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			return BadRequest(response);
		}

		result = await userManager.AddToRolesAsync(
			user,
			rolesViewModel.RolesList.Where(x => x.Selected).Select(x => x.RoleName));

		if (!result.Succeeded)
		{
			response.ErrorMessages.Add($"Error adding roles to user '{user.Id}'");
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			return BadRequest(response);
		}

		response.Result = rolesViewModel;
		response.Success = true;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}

	[HttpGet("GetUserManageClaims/{userId}", Name = "GetUserManageClaims")]
	public async Task<ActionResult<ApiResponse>> GetUserManageClaims(string userId)
	{
		var user = await userManager.FindByIdAsync(userId);
		if (user == null)
		{
			response.ErrorMessages.Add($"User '{userId}' not found");
			response.Success = false;
			response.StatusCode = HttpStatusCode.NotFound;
			return NotFound(response);
		}

		var existingUserClaims = await userManager.GetClaimsAsync(user);
		var userDto = new ClaimsViewModel
		{
			UserDto = FormatApplicationUserDto(user),
		};

		foreach (var claim in ClaimStore.ClaimsList)
		{
			var claimSelection = new ClaimSelection
			{
				ClaimType = claim.Type,
				Selected = existingUserClaims.Any(r => r.Type == claim.Type),
			};

			userDto.ClaimsList.Add(claimSelection);
		}

		response.Result = userDto;
		response.StatusCode = HttpStatusCode.OK;
		response.Success = true;
		return Ok(response);
	}

	[HttpPost("PostUserManageClaims", Name = "PostUserManageClaims")]
	public async Task<ActionResult<ApiResponse>> PostUserManageClaims([FromBody] ClaimsViewModel claimsViewModel)
	{
		var user = await userManager.FindByIdAsync(claimsViewModel.UserDto.Id);
		if (user == null)
		{
			response.ErrorMessages.Add($"User '{claimsViewModel.UserDto.Id}' not found");
			response.Success = false;
			response.StatusCode = HttpStatusCode.NotFound;
			return NotFound(response);
		}

		var oldUserClaims = await userManager.GetClaimsAsync(user);
		var result = await userManager.RemoveClaimsAsync(user, oldUserClaims);
		if (!result.Succeeded)
		{
			response.ErrorMessages.Add($"Failed to remove existing claims for user '{user.Id}'");
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			return BadRequest(response);
		}

		result = await userManager.AddClaimsAsync(
			user,
			claimsViewModel.ClaimsList
				.Where(x => x.Selected)
				.Select(x => new Claim(x.ClaimType, x.Selected.ToString())));

		if (!result.Succeeded)
		{
			response.ErrorMessages.Add($"Error adding claims to user '{user.Id}'");
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			return BadRequest(response);
		}

		response.Result = claimsViewModel;
		response.Success = true;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}

	[HttpPost("LockUnlockUser/{userId}", Name = "LockUnlockUser")]
	public async Task<ActionResult<ApiResponse>> LockUnlockUser(string userId)
	{
		var user = await userManager.FindByIdAsync(userId);
		if (user == null)
		{
			response.ErrorMessages.Add($"User '{userId}' not found");
			response.Success = false;
			response.StatusCode = HttpStatusCode.NotFound;
			return NotFound(response);
		}

		if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
		{
			var result = await userManager.SetLockoutEndDateAsync(user, null);
			if (!result.Succeeded)
			{
				response.ErrorMessages.Add($"Failed to unlock user '{user.Id}'");
				response.Success = false;
				response.StatusCode = HttpStatusCode.BadRequest;
				return BadRequest(response);
			}
		}
		else
		{
			var result = await userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow.AddYears(100));
			if (!result.Succeeded)
			{
				response.ErrorMessages.Add($"Failed to lock user '{user.Id}'");
				response.Success = false;
				response.StatusCode = HttpStatusCode.BadRequest;
				return BadRequest(response);
			}
		}

		var userDto = FormatApplicationUserDto(user);

		response.Result = userDto;
		response.Success = true;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}

	[HttpPost("DeleteUser/{userId}", Name = "DeleteUser")]
	public async Task<ActionResult<ApiResponse>> DeleteUser(string userId)
	{
		var user = await userManager.FindByIdAsync(userId);
		if (user == null)
		{
			response.ErrorMessages.Add($"User '{userId}' not found");
			response.Success = false;
			response.StatusCode = HttpStatusCode.NotFound;
			return NotFound(response);
		}

		var result = await userManager.DeleteAsync(user);
		if (!result.Succeeded)
		{
			response.ErrorMessages.Add($"Failed to delete user '{user.Id}'");
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			return BadRequest(response);
		}

		var deleteMessage = $"User '{user.Id}' deleted successfully";
		response.Result = deleteMessage;
		response.Success = true;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}

	private static ApplicationUserDto FormatApplicationUserDto(ApplicationUser user)
	{
		return new ApplicationUserDto
		{
			Id = user.Id,
			UserName = user.UserName,
			Email = user.Email,
			PhoneNumber = user.PhoneNumber,
			LastName = user.LastName,
			FirstName = user.FirstName,
			LockoutEnabled = user.LockoutEnabled,
			LockoutEnd = user.LockoutEnd,
			AccessFailedCount = user.AccessFailedCount,
		};
	}
}