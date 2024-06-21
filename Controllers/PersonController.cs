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
			.FirstOrDefaultAsync(p => p.PersonId == personId);
		if (person == null)
		{
			response.ErrorMessages.Add($"Person with id '{personId}' not found");
			response.Success = false;
			response.StatusCode = HttpStatusCode.NotFound;
			return NotFound(response);
		}

		var personResponse = new PersonDto
		{
			Person = person,
			Addresses = [],
			Phones = [],
			Emails = [],
		};

		personResponse.Addresses = await db.PersonAddressLookups
			.Include(a => a.Address)
			.Where(a => a.PersonId == personId)
			.Select(a => a.Address).ToListAsync();

		personResponse.Phones = await db.PersonPhoneLookups
			.Include(a => a.Phone)
			.Where(a => a.PersonId == personId)
			.Select(a => a.Phone).ToListAsync();

		personResponse.Emails = await db.PersonEmailLookups
			.Include(a => a.Email)
			.Where(a => a.PersonId == personId)
			.Select(a => a.Email).ToListAsync();


		response.Result = personResponse;
		response.Success = true;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}

}