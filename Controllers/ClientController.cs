using CustomerServiceApi.Data;
using CustomerServiceApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace CustomerServiceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController(ApplicationDbContext db) : ControllerBase
{
	private readonly ApiResponse response = new();

	[HttpGet("GetClients", Name = "GetClients")]
	public async Task<ActionResult<ApiResponse>> GetClients()
	{
		response.Result = await db.Clients.ToListAsync();
		response.Success = true;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}

	[HttpGet("GetClient/{clientId:guid}", Name = "GetClient")]
	public async Task<ActionResult<ApiResponse>> GetClient(Guid clientId)
	{
		var client = await db.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId);
		if (client == null)
		{
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			response.ErrorMessages.Add($"Client '{clientId}' not found");
			return BadRequest(response);
		}

		response.Result = client;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}

	[HttpPost("PostClient", Name = "PostClient")]
	public async Task<ActionResult<ApiResponse>> PostClient([FromBody] Client client)
	{
		try
		{
			if (ModelState.IsValid)
			{
				await db.Clients.AddAsync(client);
				await db.SaveChangesAsync();
				response.Result = client;
				response.StatusCode = HttpStatusCode.Created;
				response.Success = true;
				return CreatedAtRoute("GetClient", new {clientId = client.ClientId}, response);
			}

			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			response.ErrorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
			return BadRequest(response);
		}
		catch (Exception e)
		{
			response.Success = false;
			response.StatusCode = HttpStatusCode.InternalServerError;
			response.ErrorMessages.Add(e.Message);
			return StatusCode(StatusCodes.Status500InternalServerError, response);
		}
	}

	[HttpPut("/PutClient/{clientId:guid}", Name = "PutClient")]
	public async Task<ActionResult<ApiResponse>> PutClient(Guid clientId, [FromBody] Client client)
	{
		var dbClient = await db.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId);
		if (dbClient == null)
		{
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			response.ErrorMessages.Add($"Client '{clientId}' not found");
			return BadRequest(response);
		}

		dbClient.ClientName = client.ClientName;
		dbClient.Active = client.Active;
		dbClient.ClientTypeId = client.ClientTypeId;
		dbClient.EnteredBy = User.FindFirstValue(ClaimTypes.Name);

		try
		{
			await db.SaveChangesAsync();
			response.Result = dbClient;
			response.StatusCode = HttpStatusCode.OK;
			response.Success = true;
			return Ok(response);
		}
		catch (Exception e)
		{
			response.Success = false;
			response.StatusCode = HttpStatusCode.InternalServerError;
			response.ErrorMessages.Add(e.Message);
			return StatusCode(StatusCodes.Status500InternalServerError, response);
		}
	}
}