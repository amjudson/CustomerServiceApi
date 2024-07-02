using CustomerServiceApi.Data;
using CustomerServiceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CustomerServiceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenderController(ApplicationDbContext db) : ControllerBase
{
	private readonly ApiResponse response = new();

	[HttpGet("GetGenders")]
	public async Task<ActionResult<ApiResponse>> GetGenders()
	{
		response.Result = await db.Genders.ToListAsync();
		response.Success = true;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}

	[HttpPost("CreateGender", Name = "CreateGender")]
	public async Task<ActionResult<ApiResponse>> CreateGender([FromBody] Gender gender)
	{
		if (gender == null)
		{
			response.ErrorMessages.Add("Gender object is null");
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			return BadRequest(response);
		}

		try
		{
			await db.Genders.AddAsync(gender);
			await db.SaveChangesAsync();

			response.Result = gender;
			response.Success = true;
			response.StatusCode = HttpStatusCode.OK;
			return Ok(response);
		}
		catch (Exception e)
		{
			response.ErrorMessages.Add(e.Message);
			response.Success = false;
			response.StatusCode = HttpStatusCode.InternalServerError;
			return BadRequest(response);
		}
	}

	[HttpPut("UpdateGender/{genderId:int}")]
	public async Task<ActionResult<ApiResponse>> UpdateGender(int genderId, [FromBody] Gender gender)
	{
		if (gender == null)
		{
			response.ErrorMessages.Add("Gender object is null");
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			return BadRequest(response);
		}

		var dbGender = await db.Genders.FirstOrDefaultAsync(g => g.GenderId == genderId);
		if (dbGender == null)
		{
			response.ErrorMessages.Add($"Gender with id '{genderId}' not found");
			response.Success = false;
			response.StatusCode = HttpStatusCode.NotFound;
			return NotFound(response);
		}

		dbGender.Description = gender.Description;
		dbGender.Name = gender.Name;
		try
		{
			await db.SaveChangesAsync();

			response.Result = dbGender;
			response.Success = true;
			response.StatusCode = HttpStatusCode.OK;
			return Ok(response);
		}
		catch (Exception e)
		{
			response.ErrorMessages.Add(e.Message);
			response.Success = false;
			response.StatusCode = HttpStatusCode.InternalServerError;
			return BadRequest(response);
		}
	}
}