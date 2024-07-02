using CustomerServiceApi.Data;
using CustomerServiceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CustomerServiceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientTypeController(ApplicationDbContext db) : ControllerBase
{
	private readonly ApiResponse response = new();

	[HttpGet("GetClientTypes")]
	public async Task<ActionResult<ApiResponse>> GetClientTypes()
	{
		response.Result = await db.ClientTypes.ToListAsync();
		response.StatusCode = HttpStatusCode.OK;
		response.Success = true;
		return Ok(response);
	}

	[HttpGet("GetClientTypeById/{clientTypeId:int}")]
	public async Task<ActionResult<ApiResponse>> GetClientTypeById(int clientTypeId)
	{
		var dbClientType = await db.ClientTypes.FirstOrDefaultAsync(c => c.ClientTypeId == clientTypeId);
		if (dbClientType == null)
		{
			response.ErrorMessages.Add($"ClientType with id '{clientTypeId}' not found");
			response.StatusCode = HttpStatusCode.NotFound;
			response.Success = false;
			return NotFound(response);
		}

		response.Result = dbClientType;
		response.StatusCode = HttpStatusCode.OK;
		response.Success = true;
		return Ok(response);
	}

	[HttpPost("CreateClientType", Name = "CreateClientType")]
	public async Task<ActionResult<ApiResponse>> CreateClientType([FromBody] ClientType clientType)
	{
		if (clientType == null)
		{
			response.ErrorMessages.Add("ClientType object is null");
			response.StatusCode = HttpStatusCode.InternalServerError;
			response.Success = false;
			return BadRequest(response);
		}

		try
		{
			await db.ClientTypes.AddAsync(clientType);
			await db.SaveChangesAsync();
			response.Result = clientType;
			response.StatusCode = HttpStatusCode.OK;
			response.Success = true;
			return Ok(response);
		}
		catch (Exception ex)
		{
			response.ErrorMessages.Add(ex.Message);
			response.StatusCode = HttpStatusCode.InternalServerError;
			response.Success = false;
			return BadRequest(response);
		}
	}

	[HttpPut("UpdateClientType/{clientTypeId:int}")]
	public async Task<ActionResult<ApiResponse>> UpdateClientType(int clientTypeId, [FromBody] ClientType clientType)
	{
		if (clientType == null || clientTypeId != clientType.ClientTypeId)
		{
			response.ErrorMessages.Add("ClientType object is null or ClientTypeId does not match");
			response.StatusCode = HttpStatusCode.InternalServerError;
			response.Success = false;
			return BadRequest(response);
		}

		var dbClientType = await db.ClientTypes.FirstOrDefaultAsync(c => c.ClientTypeId == clientTypeId);
		if (dbClientType == null)
		{
			response.ErrorMessages.Add($"ClientType with id '{clientTypeId}' not found");
			response.StatusCode = HttpStatusCode.NotFound;
			response.Success = false;
			return NotFound(response);
		}

		dbClientType.Description = clientType.Description;
		dbClientType.Name = clientType.Name;
		try
		{
			await db.SaveChangesAsync();
			response.Result = clientType;
			response.StatusCode = HttpStatusCode.OK;
			response.Success = true;
			return Ok(response);
		}
		catch (Exception ex)
		{
			response.ErrorMessages.Add(ex.Message);
			response.StatusCode = HttpStatusCode.InternalServerError;
			response.Success = false;
			return BadRequest(response);
		}
	}
}