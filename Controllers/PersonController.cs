using CustomerServiceApi.Data;
using CustomerServiceApi.Models;
using CustomerServiceApi.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CustomerServiceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController(ApplicationDbContext db) : ControllerBase
{
	private readonly ApiResponse response = new();

	[HttpGet("GetPeopleList", Name = "GetPeopleList")]
	public async Task<ActionResult<ApiResponse>> GetPeopleList()
	{
		response.Result = await db.People.ToListAsync();
		response.Success = true;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}

	[HttpGet("GetPersonById/{personId:int}", Name = "GetPersonById")]
	public async Task<ActionResult<ApiResponse>> GetPersonById(int personId)
	{
		var person = await db.People
			.Include(p => p.Client)
			.Include(p => p.Gender)
			.Include(p => p.Race)
			.FirstOrDefaultAsync(p => p.PersonId == personId);
		if (person == null)
		{
			response.ErrorMessages.Add("Person not found");
			response.Success = false;
			response.StatusCode = HttpStatusCode.NotFound;
			return NotFound(response);
		}

		var personResponse = new PersonResponseDto
		{
			Person = new Person(),
			Addresses = [],
			Phones = [],
			Emails = [],
		};
		var addresses = db.PersonAddressLookups
			.Include(a => a.Address)
			.Where(a => a.PersonId == personId)
			.Select(a => a.Address);


		if (addresses.Any())
		{
			foreach (var addr in addresses)
			{
				var addressType = db.AddressTypes.First(a => a.AddressTypeId == addr.AddressTypeId);
				var state = db.States.First(s => s.StateId == addr.StateId);
				var addressDto = new AddressResponseDto
				{
					AddressTypeId = addr.AddressId,
					AddressType = addressType.Name,
					AddressLine1 = addr.AddressLine1,
					AddressLine2 = addr.AddressLine2,
					City = addr.City,
					State = state.Abbreviation,
					AddressId = addr.AddressId,
					Zip = addr.Zip,
				};

				personResponse.Addresses.Add(addressDto);
			}
		}

		var phones = db.PersonPhoneLookups
			.Include(a => a.Phone)
			.Where(a => a.PersonId == personId)
			.Select(a => a.Phone);

		var emails = db.PersonEmailLookups
			.Include(a => a.Email)
			.Where(a => a.PersonId == personId)
			.Select(a => a.Email);


		response.Result = personResponse;
		response.Success = true;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}

}