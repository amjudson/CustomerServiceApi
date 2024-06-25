using CustomerServiceApi.Data;
using CustomerServiceApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CustomerServiceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhoneController(ApplicationDbContext db) : ControllerBase
{
	private readonly ApiResponse response = new();

	[HttpGet("GetPhones", Name = "GetPhones")]
	public async Task<ActionResult<ApiResponse>> GetPhones()
	{
		response.Result = await db.Phones.ToListAsync();
		response.Success = true;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}

	[HttpGet("GetPhoneById/{phoneId:int}", Name = "GetPhoneById")]
	public async Task<ActionResult<ApiResponse>> GetPhoneById(int phoneId)
	{
		var phone = await db.Phones.FirstOrDefaultAsync(p => p.PhoneId == phoneId);
		if (phone == null)
		{
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			response.ErrorMessages.Add($"Phone '{phoneId}' not found");
			return BadRequest(response);
		}

		response.Result = phone;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}

	[HttpPost("AddPhone", Name = "AddPhone")]
	public async Task<ActionResult<ApiResponse>> AddPhone([FromBody] Phone phone)
	{
		if (phone == null)
		{
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			response.ErrorMessages.Add("Phone object is null");
			return BadRequest(response);
		}

		await db.Phones.AddAsync(phone);
		await db.SaveChangesAsync();

		response.Result = phone;
		response.Success = true;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}

	[HttpPut("UpdatePhone/{phoneId:int}", Name = "UpdatePhone")]
	public async Task<ActionResult<ApiResponse>> UpdatePhone(int phoneId, [FromBody] Phone phone)
	{
		var dbPhone = await db.Phones.FirstOrDefaultAsync(p => p.PhoneId == phoneId);
		if (dbPhone == null)
		{
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			response.ErrorMessages.Add($"Phone '{phoneId}' not found");
			return BadRequest(response);
		}

		if (phone == null)
		{
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			response.ErrorMessages.Add("Phone object is null");
			return BadRequest(response);
		}

		dbPhone.PhoneNumber = phone.PhoneNumber;
		dbPhone.PhoneType = phone.PhoneType;
		dbPhone.Extension = phone.Extension;

		db.Phones.Update(dbPhone);
		await db.SaveChangesAsync();

		response.Result = dbPhone;
		response.Success = true;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}
}