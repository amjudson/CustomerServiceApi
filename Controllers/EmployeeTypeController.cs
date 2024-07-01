using CustomerServiceApi.Data;
using CustomerServiceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CustomerServiceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeTypeController(ApplicationDbContext db) : ControllerBase
{
	private readonly ApiResponse response = new();

	[HttpGet("GetEmployeeTypes")]
	public async Task<ActionResult<ApiResponse>> GetEmployeeTypes()
	{
		response.Data = await db.EmployeeTypes.ToListAsync();
		response.StatusCode = HttpStatusCode.OK;
		response.Success = true;
		return Ok(response);
	}

	[HttpGet("GetEmployeeTypeById/{employeeTypeId:int}")]
	public async Task<ActionResult<ApiResponse>> GetEmployeeTypeById(int employeeTypeId)
	{
		var dbEmployeeType = await db.EmployeeTypes.FirstOrDefaultAsync(e => e.EmployeeTypeId == employeeTypeId);
		if (dbEmployeeType == null)
		{
			response.ErrorMessages.Add($"EmployeeType with id '{employeeTypeId}' not found");
			response.StatusCode = HttpStatusCode.NotFound;
			response.Success = false;
			return NotFound(response);
		}

		response.Data = dbEmployeeType;
		response.StatusCode = HttpStatusCode.OK;
		response.Success = true;
		return Ok(response);
	}

	[HttpPost("CreateEmployeeType", Name = "CreateEmployeeType")]
	public async Task<ActionResult<ApiResponse>> CreateEmployeeType([FromBody] EmployeeType employeeType)
	{
		if (employeeType == null)
		{
			response.ErrorMessages.Add("EmployeeType object is null");
			response.StatusCode = HttpStatusCode.InternalServerError;
			response.Success = false;
			return BadRequest(response);
		}

		try
		{
			await db.EmployeeTypes.AddAsync(employeeType);
			await db.SaveChangesAsync();
			response.Data = employeeType;
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

	[HttpPut("UpdateEmployeeType/{employeeTypeId:int}")]
	public async Task<ActionResult<ApiResponse>> UpdateEmployeeType(int employeeTypeId, [FromBody] EmployeeType employeeType)
	{
		if (employeeType == null || employeeTypeId != employeeType.EmployeeTypeId)
		{
			response.ErrorMessages.Add("EmployeeType object is null or EmployeeTypeId does not match");
			response.StatusCode = HttpStatusCode.BadRequest;
			response.Success = false;
			return BadRequest(response);
		}

		var dbEmployeeType = await db.EmployeeTypes.FirstOrDefaultAsync(e => e.EmployeeTypeId == employeeTypeId);
		if (dbEmployeeType == null)
		{
			response.ErrorMessages.Add($"EmployeeType with id '{employeeTypeId}' not found");
			response.StatusCode = HttpStatusCode.NotFound;
			response.Success = false;
			return NotFound(response);
		}

		dbEmployeeType.Description = employeeType.Description;
		dbEmployeeType.Name = employeeType.Name;
		dbEmployeeType.ClientId = employeeType.ClientId;

		try
		{
			await db.SaveChangesAsync();
			response.Data = dbEmployeeType;
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