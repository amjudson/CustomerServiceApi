using CustomerServiceApi.Data;
using CustomerServiceApi.Models;
using CustomerServiceApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CustomerServiceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController(ApplicationDbContext db) : ControllerBase
{
	private readonly ApiResponse response = new();

	[HttpGet("GetClientList", Name = "GetClientList")]
	public async Task<ActionResult<ApiResponse>> GetClientList()
	{
		response.Data = await db.Clients.Where(c => c.Active).ToListAsync();
		response.Success = true;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}

	[HttpGet("GetClientById/{clientId:guid}", Name = "GetClientById")]
	public async Task<ActionResult<ApiResponse>> GetClientById(Guid clientId)
	{
		var client = await db.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId);
		if (client == null)
		{
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			response.ErrorMessages.Add($"Client '{clientId}' not found");
			return BadRequest(response);
		}

		var clientResponse = new ClientDto
		{
			Client = client,
			Addresses = [],
			Phones = [],
			Emails = [],
		};

		clientResponse.Addresses = await db.ClientAddressLookups
			.Include(a => a.Address)
			.Where(a => a.ClientId == clientId)
			.Select(a => a.Address).ToListAsync();

		clientResponse.Phones = await db.ClientPhoneLookups
			.Include(a => a.Phone)
			.Where(a => a.ClientId == clientId)
			.Select(a => a.Phone).ToListAsync();

		clientResponse.Emails = await db.ClientEmailLookups
			.Include(a => a.Email)
			.Where(a => a.ClientId == clientId)
			.Select(a => a.Email).ToListAsync();

		response.Data = clientResponse;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}

	[HttpPost("PostClient", Name = "PostClient")]
	public async Task<ActionResult<ApiResponse>> PostClient([FromBody] ClientDto clientDto)
	{
		var responseClient = new ClientDto
		{
			Client = new Client(),
			Addresses = [],
			Phones = [],
			Emails = [],
		};

		try
		{
			var client = clientDto.Client;
			if (ModelState.IsValid)
			{
				await db.Clients.AddAsync(client);
				await db.SaveChangesAsync();
			}
			else
			{
				response.Success = false;
				response.StatusCode = HttpStatusCode.BadRequest;
				response.ErrorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
				return BadRequest(response);
			}

			responseClient.Client = client;
			foreach (var address in clientDto.Addresses)
			{
				var existingAddress = await db.Addresses
					.FirstOrDefaultAsync(a => a.AddressId == address.AddressId);
				if (existingAddress == null)
				{
					await db.Addresses.AddAsync(address);
					await db.SaveChangesAsync();
				}
				else
				{
					existingAddress.AddressLine1 = address.AddressLine1;
					existingAddress.AddressLine2 = address.AddressLine2;
					existingAddress.AddressTypeId = address.AddressTypeId;
					existingAddress.City = address.City;
					existingAddress.StateId = address.StateId;
					existingAddress.Zip = address.Zip;
					await db.SaveChangesAsync();
				}

				if (!await db.ClientAddressLookups
					.AnyAsync(a => a.ClientId == client.ClientId && a.AddressId == address.AddressId))
				{
					await db.ClientAddressLookups.AddAsync(new ClientAddressLookup
					{
						ClientId = client.ClientId,
						AddressId = address.AddressId,
					});
				}

				responseClient.Addresses.Add(address);
			}

			foreach (var email in clientDto.Emails)
			{
				var existingEmail = await db.Emails
					.FirstOrDefaultAsync(a => a.EmailId == email.EmailId);
				if (existingEmail == null)
				{
					await db.Emails.AddAsync(email);
					await db.SaveChangesAsync();
				}
				else
				{
					existingEmail.EmailAddress = email.EmailAddress;
					existingEmail.EmailTypeId = email.EmailTypeId;
					await db.SaveChangesAsync();
				}

				if (!await db.ClientEmailLookups
					    .AnyAsync(a => a.ClientId == client.ClientId && a.EmailId == email.EmailId))
				{
					await db.ClientEmailLookups.AddAsync(new ClientEmailLookup
					{
						ClientId = client.ClientId,
						EmailId = email.EmailId,
					});
				}

				responseClient.Emails.Add(email);
			}

			foreach (var phone in clientDto.Phones)
			{
				var existingPhone = await db.Phones
					.FirstOrDefaultAsync(a => a.PhoneId == phone.PhoneId);
				if (existingPhone == null)
				{
					await db.Phones.AddAsync(phone);
					await db.SaveChangesAsync();
				}
				else
				{
					existingPhone.PhoneNumber = phone.PhoneNumber;
					existingPhone.PhoneTypeId = phone.PhoneTypeId;
					await db.SaveChangesAsync();
				}

				if (!await db.ClientPhoneLookups
					    .AnyAsync(a => a.ClientId == client.ClientId && a.PhoneId == phone.PhoneId))
				{
					await db.ClientPhoneLookups.AddAsync(new ClientPhoneLookup
					{
						ClientId = client.ClientId,
						PhoneId = phone.PhoneId,
					});
				}

				responseClient.Phones.Add(phone);
			}

			await db.SaveChangesAsync();
			response.Data = responseClient;
			response.Success = true;
			response.StatusCode = HttpStatusCode.Created;
			return CreatedAtRoute("GetClientById", new { clientId = client.ClientId }, response);
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
	public async Task<ActionResult<ApiResponse>> PutClient(Guid clientId, [FromBody] ClientDto clientDto)
	{
		var dbClient = await db.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId);
		if (dbClient == null)
		{
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			response.ErrorMessages.Add($"Client '{clientId}' not found");
			return BadRequest(response);
		}

		var responseClient = new ClientDto
		{
			Client = new Client(),
			Addresses = [],
			Phones = [],
			Emails = [],
		};
		try
		{
			dbClient.ClientName = clientDto.Client.ClientName;
			dbClient.Active = clientDto.Client.Active;
			dbClient.ClientTypeId = clientDto.Client.ClientTypeId;
			// dbClient.EnteredBy = User.FindFirstValue(ClaimTypes.Name);
			dbClient.EnteredBy = "system";
			responseClient.Client = dbClient;
			foreach (var address in clientDto.Addresses)
			{
				var existingAddress = await db.Addresses
					.FirstOrDefaultAsync(a => a.AddressId == address.AddressId);
				if (existingAddress == null)
				{
					await db.Addresses.AddAsync(address);
					await db.SaveChangesAsync();
				}
				else
				{
					existingAddress.AddressLine1 = address.AddressLine1;
					existingAddress.AddressLine2 = address.AddressLine2;
					existingAddress.AddressTypeId = address.AddressTypeId;
					existingAddress.City = address.City;
					existingAddress.StateId = address.StateId;
					existingAddress.Zip = address.Zip;
					await db.SaveChangesAsync();
				}

				if (!await db.ClientAddressLookups
					.AnyAsync(a => a.ClientId == clientId && a.AddressId == address.AddressId))
				{
					await db.ClientAddressLookups.AddAsync(new ClientAddressLookup
					{
						ClientId = clientId,
						AddressId = address.AddressId,
					});
				}

				responseClient.Addresses.Add(address);
			}

			foreach (var email in clientDto.Emails)
			{
				var existingEmail = await db.Emails
					.FirstOrDefaultAsync(a => a.EmailId == email.EmailId);
				if (existingEmail == null)
				{
					await db.Emails.AddAsync(email);
					await db.SaveChangesAsync();
				}
				else
				{
					existingEmail.EmailAddress = email.EmailAddress;
					existingEmail.EmailTypeId = email.EmailTypeId;
					await db.SaveChangesAsync();
				}

				if (!await db.ClientEmailLookups
					    .AnyAsync(a => a.ClientId == clientId && a.EmailId == email.EmailId))
				{
					await db.ClientEmailLookups.AddAsync(new ClientEmailLookup
					{
						ClientId = clientId,
						EmailId = email.EmailId,
					});
				}

				responseClient.Emails.Add(email);
			}

			foreach (var phone in clientDto.Phones)
			{
				var existingPhone = await db.Phones
					.FirstOrDefaultAsync(a => a.PhoneId == phone.PhoneId);
				if (existingPhone == null)
				{
					await db.Phones.AddAsync(phone);
					await db.SaveChangesAsync();
				}
				else
				{
					existingPhone.PhoneNumber = phone.PhoneNumber;
					existingPhone.PhoneTypeId = phone.PhoneTypeId;
					await db.SaveChangesAsync();
				}

				if (!await db.ClientPhoneLookups
					    .AnyAsync(a => a.ClientId == clientId && a.PhoneId == phone.PhoneId))
				{
					await db.ClientPhoneLookups.AddAsync(new ClientPhoneLookup
					{
						ClientId = clientId,
						PhoneId = phone.PhoneId,
					});
				}

				responseClient.Phones.Add(phone);
			}

			await db.SaveChangesAsync();
			response.Data = responseClient;
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