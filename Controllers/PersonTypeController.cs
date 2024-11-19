using CustomerServiceApi.Data;
using CustomerServiceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CustomerServiceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonTypeController(ApplicationDbContext db) : ControllerBase
{
	private readonly ApiResponse response = new();

	[HttpGet("GetPersonTypes")]
	public async Task<ActionResult<ApiResponse>> GetPersonTypes()
	{
		response.Result = await db.PersonTypes.Where(pt => !pt.ClientOption).ToListAsync();
		response.StatusCode = HttpStatusCode.OK;
		response.Success = true;
		return Ok(response);
	}

	[HttpGet("GetClientPersonTypes")]
	public async Task<ActionResult<ApiResponse>> GetClientPersonTypes()
	{
		response.Result = await db.PersonTypes.Where(pt => pt.ClientOption).ToListAsync();
		response.StatusCode = HttpStatusCode.OK;
		response.Success = true;
		return Ok(response);
	}

	[HttpPost("CreatePersonType", Name = "CreatePersonType")]
	public async Task<ActionResult<ApiResponse>> CreatePhoneType([FromBody] PersonType personType)
	{
		if (personType == null)
		{
			response.ErrorMessages.Add("PersonType object is null");
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			return BadRequest(response);
		}

		try
		{
			await db.PersonTypes.AddAsync(personType);
			await db.SaveChangesAsync();

			response.Result = personType;
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

	[HttpPut("UpdatePersonType/{personTypeId:int}", Name = "UpdatePersonType")]
	public async Task<ActionResult<ApiResponse>> UpdatePhoneType(int personTypeId, [FromBody] PersonType personType)
	{
		if (personType == null)
		{
			response.ErrorMessages.Add("PhoneType object is null");
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			return BadRequest(response);
		}

		var dbPersonType = await db.PersonTypes.FirstOrDefaultAsync(pt => pt.PersonTypeId == personTypeId);
		if (dbPersonType == null)
		{
			response.ErrorMessages.Add($"PhoneType with id '{personTypeId}' not found");
			response.Success = false;
			response.StatusCode = HttpStatusCode.NotFound;
			return NotFound(response);
		}

		dbPersonType.Description = personType.Description;
		dbPersonType.ClientOption = personType.ClientOption;
		dbPersonType.Name = personType.Name;
		dbPersonType.ClientId = personType.ClientId;
		try
		{
			await db.SaveChangesAsync();

			response.Result = dbPersonType;
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