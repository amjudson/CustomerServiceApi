using CustomerServiceApi.Data;
using CustomerServiceApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CustomerServiceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressTypeController(ApplicationDbContext db) : ControllerBase
{
	private readonly ApiResponse response = new ApiResponse();

	[HttpGet("GetAddressTypes")]
	public async Task<ActionResult<ApiResponse>> GetAddressTypes()
	{
		response.Data = await db.AddressTypes.Where(at => !at.ClientOption).ToListAsync();
		response.StatusCode = HttpStatusCode.OK;
		response.Success = true;
		return Ok(response);
	}

	[HttpGet("GetClientAddressTypes")]
	public async Task<ActionResult<ApiResponse>> GetClientAddressTypes()
	{
		response.Data = await db.AddressTypes.Where(at => at.ClientOption).ToListAsync();
		response.StatusCode = HttpStatusCode.OK;
		response.Success = true;
		return Ok(response);
	}

	[HttpPut("UpdateAddressType/{addressTypeId:int}")]
	public async Task<ActionResult<ApiResponse>> UpdateAddressType(int addressTypeId, [FromBody] AddressType addressType)
	{
		if (addressType == null)
		{
			response.ErrorMessages.Add("AddressType object is null");
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			return BadRequest(response);
		}

		var dbAddressType = await db.AddressTypes.FirstOrDefaultAsync(at => at.AddressTypeId == addressTypeId);
		if (dbAddressType == null)
		{
			response.ErrorMessages.Add($"AddressType with id '{addressTypeId}' not found");
			response.Success = false;
			response.StatusCode = HttpStatusCode.NotFound;
			return NotFound(response);
		}

		dbAddressType.Description = addressType.Description;
		dbAddressType.ClientOption = addressType.ClientOption;
		dbAddressType.Name = addressType.Name;
		dbAddressType.ClientId = addressType.ClientId;

		await db.SaveChangesAsync();

		response.Data = dbAddressType;
		response.Success = true;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}

	[HttpPost("CreateAddressType")]
	public async Task<ActionResult<ApiResponse>> CreateAddressType([FromBody] AddressType addressType)
	{
		if (addressType == null)
		{
			response.ErrorMessages.Add("AddressType object is null");
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			return BadRequest(response);
		}

		try
		{
			db.AddressTypes.Add(addressType);
			await db.SaveChangesAsync();

			response.Data = addressType;
			response.Success = true;
			response.StatusCode = HttpStatusCode.OK;
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