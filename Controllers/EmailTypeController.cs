using CustomerServiceApi.Data;
using CustomerServiceApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CustomerServiceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailTypeController(ApplicationDbContext db) : ControllerBase
{
	private readonly ApiResponse response = new();

	[HttpGet("GetEmailTypes")]
	public async Task<ActionResult<ApiResponse>> GetEmailTypes()
	{
		response.Result = await db.EmailTypes.Where(et => !et.ClientOption).ToListAsync();
		response.StatusCode = HttpStatusCode.OK;
		response.Success = true;
		return Ok(response);
	}

	[HttpGet("GetClientEmailTypes")]
	public async Task<ActionResult<ApiResponse>> GetClientEmailTypes()
	{
		response.Result = await db.EmailTypes.Where(et => et.ClientOption).ToListAsync();
		response.StatusCode = HttpStatusCode.OK;
		response.Success = true;
		return Ok(response);
	}

	[HttpPost("CreateEmailType", Name = "CreateEmailType")]
	public async Task<ActionResult<ApiResponse>> CreateEmailType([FromBody] EmailType emailType)
	{
		if (emailType == null)
		{
			response.ErrorMessages.Add("EmailType object is null");
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			return BadRequest(response);
		}

		try
		{
			await db.EmailTypes.AddAsync(emailType);
			await db.SaveChangesAsync();

			response.Result = emailType;
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

	[HttpPut("UpdateEmailType/{emailTypeId:int}", Name = "UpdateEmailType")]
	public async Task<ActionResult<ApiResponse>> UpdateEmailType(int emailTypeId, [FromBody] EmailType emailType)
	{
		if (emailType == null)
		{
			response.ErrorMessages.Add("EmailType object is null");
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			return BadRequest(response);
		}

		var dbEmailType = await db.EmailTypes.FirstOrDefaultAsync(et => et.EmailTypeId == emailTypeId);
		if (dbEmailType == null)
		{
			response.ErrorMessages.Add($"EmailType with id '{emailTypeId}' not found");
			response.Success = false;
			response.StatusCode = HttpStatusCode.NotFound;
			return NotFound(response);
		}

		dbEmailType.Description = emailType.Description;
		dbEmailType.ClientOption = emailType.ClientOption;
		dbEmailType.Name = emailType.Name;
		dbEmailType.ClientId = emailType.ClientId;

		try
		{
			await db.SaveChangesAsync();
			response.Result = dbEmailType;
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