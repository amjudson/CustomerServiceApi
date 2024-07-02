using CustomerServiceApi.Data;
using CustomerServiceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CustomerServiceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RaceController(ApplicationDbContext db) : ControllerBase
{
	private readonly ApiResponse response = new();

	[HttpGet("GetRaces")]
	public async Task<ActionResult<ApiResponse>> GetRaces()
	{
		response.Result = await db.Races.ToListAsync();
		response.StatusCode = HttpStatusCode.OK;
		response.Success = true;
		return Ok(response);
	}

	[HttpGet("GetRaceById/{raceId:int}")]
	public async Task<ActionResult<ApiResponse>> GetRaceById(int raceId)
	{
		var dbRace = await db.Races.FirstOrDefaultAsync(r => r.RaceId == raceId);
		if (dbRace == null)
		{
			response.ErrorMessages.Add($"Race with id '{raceId}' not found");
			response.StatusCode = HttpStatusCode.NotFound;
			response.Success = false;
			return NotFound(response);
		}

		response.Result = dbRace;
		response.StatusCode = HttpStatusCode.OK;
		response.Success = true;
		return Ok(response);
	}

	[HttpPost("CreateRace", Name = "CreateRace")]
	public async Task<ActionResult<ApiResponse>> CreateRace([FromBody] Race race)
	{
		if (race == null)
		{
			response.ErrorMessages.Add("Race object is null");
			response.StatusCode = HttpStatusCode.InternalServerError;
			response.Success = false;
			return BadRequest(response);
		}

		try
		{
			await db.Races.AddAsync(race);
			await db.SaveChangesAsync();
			response.Result = race;
			response.StatusCode = HttpStatusCode.OK;
			response.Success = true;
			return Ok(response);
		}
		catch (Exception e)
		{
			response.ErrorMessages.Add(e.Message);
			response.StatusCode = HttpStatusCode.InternalServerError;
			response.Success = false;
			return BadRequest(response);
		}
	}

	[HttpPut("UpdateRace/{raceId:int}")]
	public async Task<ActionResult<ApiResponse>> UpdateRace(int raceId, [FromBody] Race race)
	{
		if (race == null)
		{
			response.ErrorMessages.Add("Race object is null");
			response.StatusCode = HttpStatusCode.InternalServerError;
			response.Success = false;
			return BadRequest(response);
		}

		var dbRace = await db.Races.FirstOrDefaultAsync(r => r.RaceId == raceId);
		if (dbRace == null)
		{
			response.ErrorMessages.Add($"Race with id '{raceId}' not found");
			response.StatusCode = HttpStatusCode.NotFound;
			response.Success = false;
			return NotFound(response);
		}

		dbRace.Description = dbRace.Description;
		dbRace.Name = dbRace.Name;
		try
		{
			await db.SaveChangesAsync();
			response.Result = dbRace;
			response.StatusCode = HttpStatusCode.OK;
			response.Success = true;
			return Ok(response);
		}
		catch (Exception e)
		{
			response.ErrorMessages.Add(e.Message);
			response.StatusCode = HttpStatusCode.InternalServerError;
			response.Success = false;
			return BadRequest(response);
		}
	}
}