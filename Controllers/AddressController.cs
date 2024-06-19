using CustomerServiceApi.Data;
using CustomerServiceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CustomerServiceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController(ApplicationDbContext db) : ControllerBase
{
	private readonly ApiResponse response = new();

	[HttpGet("GetAddresses", Name = "GetAddresses")]
	public async Task<ActionResult<ApiResponse>> GetAddresses()
    {
        response.Result = await db.Addresses.ToListAsync();
        response.Success = true;
        response.StatusCode = HttpStatusCode.OK;
        return Ok(response);
    }

	[HttpGet("GetAddressById/{addressId:int}", Name = "GetAddressById")]
	public async Task<ActionResult<ApiResponse>> GetAddressById(int addressId)
    {
        var address = await db.Addresses.FirstOrDefaultAsync(a => a.AddressId == addressId);
        if (address == null)
        {
            response.Success = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            response.ErrorMessages.Add($"Address '{addressId}' not found");
            return BadRequest(response);
        }

        response.Result = address;
        response.StatusCode = HttpStatusCode.OK;
        return Ok(response);
    }

	[HttpGet("GetAddressesForPersonId/{personId:int}", Name = "GetAddressesForPersonId")]
	public async Task<ActionResult<ApiResponse>> GetAddressesForPersonId(int personId)
    {
        var addressMaps = await db.PersonAddressLookups
	        .Where(a => a.PersonId == personId)
	        .Select(a => a.AddressId)
	        .ToListAsync();
        if (addressMaps.Count == 0)
        {
            response.Success = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            response.ErrorMessages.Add($"No addresses found for Person '{personId}'");
            return BadRequest(response);
        }

        var addressList = new List<Address>();
        foreach (var addressId in addressMaps)
        {
	        var address = await db.Addresses.FirstOrDefaultAsync(a => a.AddressId == addressId);
            if (address != null)
            {
                addressList.Add(address);
            }
        }

        response.Result = addressList;
        response.StatusCode = HttpStatusCode.OK;
        response.Success = true;
        return Ok(response);
    }

	[HttpPut("PutAddress/{addressId:int}", Name = "PutAddress")]
	public async Task<ActionResult<ApiResponse>> PutAddress(int addressId, [FromBody] Address address)
	{
		var dbAddress = await db.Addresses.FirstOrDefaultAsync(a => a.AddressId == addressId);
		if (dbAddress == null)
		{
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			response.ErrorMessages.Add($"Address '{addressId}' not found");
			return BadRequest(response);
		}

		dbAddress.AddressLine1 = address.AddressLine1;
		dbAddress.AddressLine2 = address.AddressLine2;
		dbAddress.City = address.City;
		dbAddress.StateId = address.StateId;
		dbAddress.Zip = address.Zip;
		dbAddress.AddressTypeId = address.AddressTypeId;

		try
		{
			await db.SaveChangesAsync();
			response.Result = dbAddress;
			response.StatusCode = HttpStatusCode.OK;
			response.Success = true;
			return Ok(response);
		}
		catch (Exception e)
		{
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			response.ErrorMessages.Add("Failed to update address");
			response.ErrorMessages.Add(e.Message);
			return BadRequest(response);
		}
	}

	[HttpPost("PostAddress", Name = "PostAddress")]
	public async Task<ActionResult<ApiResponse>> PostAddress([FromBody] Address address)
	{
		var newAddress = new Address
		{
			AddressLine1 = address.AddressLine1,
			AddressLine2 = address.AddressLine2,
			City = address.City,
			StateId = address.StateId,
			Zip = address.Zip,
			AddressTypeId = address.AddressTypeId
		};

		try
		{
			await db.Addresses.AddAsync(newAddress);
			await db.SaveChangesAsync();
			response.Result = newAddress;
			response.StatusCode = HttpStatusCode.Created;
			response.Success = true;
			return CreatedAtRoute("GetAddressById", new { addressId = newAddress.AddressId }, response);
		}
		catch (Exception e)
		{
			response.Success = false;
			response.StatusCode = HttpStatusCode.BadRequest;
			response.ErrorMessages.Add("Failed to create address");
			response.ErrorMessages.Add(e.Message);
			return BadRequest(response);
		}
	}
}