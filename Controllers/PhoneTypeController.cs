using CustomerServiceApi.Data;
using CustomerServiceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CustomerServiceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhoneTypeController(ApplicationDbContext db) : ControllerBase
{
	private readonly ApiResponse response = new();

	[HttpGet("GetPhoneTypes")]
	public async Task<ActionResult<ApiResponse>> GetPhoneTypes()
	{
		response.Result = await db.PhoneTypes.Where(pt => !pt.ClientOption).ToListAsync();
		response.StatusCode = HttpStatusCode.OK;
		response.Success = true;
		return Ok(response);
	}

	[HttpGet("GetClientPhoneTypes")]
	public async Task<ActionResult<ApiResponse>> GetClientPhoneTypes()
	{
		response.Result = await db.PhoneTypes.Where(pt => pt.ClientOption).ToListAsync();
		response.StatusCode = HttpStatusCode.OK;
		response.Success = true;
		return Ok(response);
	}

	[HttpPost("CreatePhoneType", Name = "CreatePhoneType")]
	public async Task<ActionResult<ApiResponse>> CreatePhoneType([FromBody] PhoneType phoneType)
	{
		if (phoneType == null)
		{
			response.ErrorMessages.Add("PhoneType object is null");
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			return BadRequest(response);
		}

		try
		{
			await db.PhoneTypes.AddAsync(phoneType);
			await db.SaveChangesAsync();

			response.Result = phoneType;
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

	[HttpPut("UpdatePhoneType/{phoneTypeId:int}", Name = "UpdatePhoneType")]
	public async Task<ActionResult<ApiResponse>> UpdatePhoneType(int phoneTypeId, [FromBody] PhoneType phoneType)
	{
		if (phoneType == null)
		{
			response.ErrorMessages.Add("PhoneType object is null");
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			return BadRequest(response);
		}

		var dbPhoneType = await db.PhoneTypes.FirstOrDefaultAsync(pt => pt.PhoneTypeId == phoneTypeId);
		if (dbPhoneType == null)
		{
			response.ErrorMessages.Add($"PhoneType with id '{phoneTypeId}' not found");
			response.Success = false;
			response.StatusCode = HttpStatusCode.NotFound;
			return NotFound(response);
		}

		dbPhoneType.Description = phoneType.Description;
		dbPhoneType.ClientOption = phoneType.ClientOption;
		dbPhoneType.Name = phoneType.Name;
		dbPhoneType.ClientId = phoneType.ClientId;
		try
		{
			await db.SaveChangesAsync();

			response.Result = dbPhoneType;
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