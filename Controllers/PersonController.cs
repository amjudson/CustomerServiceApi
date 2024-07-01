using CustomerServiceApi.Data;
using CustomerServiceApi.Models;
using CustomerServiceApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace CustomerServiceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController(ApplicationDbContext db) : ControllerBase
{
	private readonly ApiResponse response = new();

	[HttpGet("GetPeopleList", Name = "GetPeopleList")]
	public async Task<ActionResult<ApiResponse>> GetPeopleList(string searchText = "", int pageNumber = 1, int pageSize = 5)
	{
		var peopleCount = await db.People.CountAsync();
		var pagination = new Pagination
		{
			CurrentPage = pageNumber,
			PageSize = pageSize,
			TotalRecords = peopleCount,
		};

		Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagination));

		response.Data = await db.People
			.Where(p =>
				p.FirstName.ToLower().Contains(searchText.ToLower()) ||
				p.LastName.ToLower().Contains(searchText.ToLower()) ||
				p.MiddleName.ToLower().Contains(searchText.ToLower()) ||
				p.Alias.ToLower().Contains(searchText.ToLower()))
			.OrderBy(p => p.LastName)
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize).ToListAsync();

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

		response.Data = personResponse;
		response.Success = true;
		response.StatusCode = HttpStatusCode.OK;
		return Ok(response);
	}

	[HttpPost("CreatePerson", Name = "CreatePerson")]
	public async Task<ActionResult<ApiResponse>> CreatePerson([FromBody] PersonDto personDto)
	{
		var responsePersonDto = new PersonDto
		{
			Person = new Person(),
			Addresses = [],
			Phones = [],
			Emails = [],
		};
		try
		{
			var person = personDto.Person;
			db.People.Add(person);
			await db.SaveChangesAsync();

			responsePersonDto.Person = person;
			foreach (var address in personDto.Addresses)
			{
				var existingAddress = await db.Addresses
					.FirstOrDefaultAsync(a => a.AddressId == address.AddressId);
				if (existingAddress == null)
				{
					db.Addresses.Add(address);
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

				if (!await db.PersonAddressLookups
					    .AnyAsync(a => a.PersonId == person.PersonId && a.AddressId == address.AddressId))
				{
					db.PersonAddressLookups.Add(new PersonAddressLookup
					{
						PersonId = person.PersonId,
						AddressId = address.AddressId
					});
				}

				responsePersonDto.Addresses.Add(address);
			}

			foreach (var phone in personDto.Phones)
			{
				var existingPhone = await db.Phones
					.FirstOrDefaultAsync(a => a.PhoneId == phone.PhoneId);
				if (existingPhone == null)
				{
					db.Phones.Add(phone);
					await db.SaveChangesAsync();
				}
				else
				{
					existingPhone.PhoneNumber = phone.PhoneNumber;
					existingPhone.PhoneTypeId = phone.PhoneTypeId;
					await db.SaveChangesAsync();
				}

				if (!await db.PersonPhoneLookups
					    .AnyAsync(a => a.PersonId == person.PersonId && a.PhoneId == phone.PhoneId))
				{
					db.PersonPhoneLookups.Add(new PersonPhoneLookup
					{
						PersonId = person.PersonId,
						PhoneId = phone.PhoneId
					});
				}

				responsePersonDto.Phones.Add(phone);
			}

			foreach (var email in personDto.Emails)
			{
				var existingEmail = await db.Emails
					.FirstOrDefaultAsync(a => a.EmailId == email.EmailId);
				if (existingEmail == null)
				{
					db.Emails.Add(email);
					await db.SaveChangesAsync();
				}
				else
				{
					existingEmail.EmailAddress = email.EmailAddress;
					existingEmail.EmailTypeId = email.EmailTypeId;
					await db.SaveChangesAsync();
				}

				if (!await db.PersonEmailLookups
					    .AnyAsync(a => a.PersonId == person.PersonId && a.EmailId == email.EmailId))
				{
					db.PersonEmailLookups.Add(new PersonEmailLookup
					{
						PersonId = person.PersonId,
						EmailId = email.EmailId
					});
				}

				responsePersonDto.Emails.Add(email);
			}

			await db.SaveChangesAsync();

			response.Data = responsePersonDto;
			response.Success = true;
			response.StatusCode = HttpStatusCode.Created;
			return CreatedAtRoute("GetPersonById", new {personId = person.PersonId}, response);
		}
		catch (Exception e)
		{
			response.ErrorMessages.Add(e.Message);
			response.Success = false;
			response.StatusCode = HttpStatusCode.InternalServerError;
			return StatusCode((int) HttpStatusCode.InternalServerError, response);
		}
	}

	[HttpPut("UpdatePerson/{personId:int}", Name = "UpdatePerson")]
	public async Task<ActionResult<ApiResponse>> UpdatePerson(int personId, [FromBody] PersonDto personDto)
	{
		var responsePersonDto = new PersonDto
		{
			Person = new Person(),
			Addresses = [],
			Phones = [],
			Emails = [],
		};
		try
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

			person.FirstName = personDto.Person.FirstName;
			person.LastName = personDto.Person.LastName;
			person.MiddleName = personDto.Person.MiddleName;
			person.Prefix = personDto.Person.Prefix;
			person.Suffix = personDto.Person.Suffix;
			person.Alias = personDto.Person.Alias;
			person.GenderId = personDto.Person.GenderId;
			person.RaceId = personDto.Person.RaceId;
			person.DateOfBirth = personDto.Person.DateOfBirth;
			person.PersonTypeId = personDto.Person.PersonTypeId;
			person.ClientId = personDto.Person.ClientId;

			if (personDto.Addresses.Count > 0)
			{
				foreach (var address in personDto.Addresses)
				{
					var existingAddress = await db.Addresses
						.FirstOrDefaultAsync(a => a.AddressId == address.AddressId);
					if (existingAddress == null)
					{
						db.Addresses.Add(address);
						await db.SaveChangesAsync();
						db.PersonAddressLookups.Add(new PersonAddressLookup
						{
							PersonId = person.PersonId,
							AddressId = address.AddressId
						});
					}
					else
					{
						existingAddress.AddressLine1 = address.AddressLine1;
						existingAddress.AddressLine2 = address.AddressLine2;
						existingAddress.City = address.City;
						existingAddress.StateId = address.StateId;
						existingAddress.Zip = address.Zip;
						existingAddress.AddressTypeId = address.AddressTypeId;
					}

					responsePersonDto.Addresses.Add(address);
				}
			}

			if (personDto.Phones.Count > 0)
			{
				foreach (var phone in personDto.Phones)
				{
					var existingPhone = await db.Phones
						.FirstOrDefaultAsync(a => a.PhoneId == phone.PhoneId);
					if (existingPhone == null)
					{
						db.Phones.Add(phone);
						await db.SaveChangesAsync();
						db.PersonPhoneLookups.Add(new PersonPhoneLookup()
						{
							PersonId = person.PersonId,
							PhoneId = phone.PhoneId
						});
					}
					else
					{
						existingPhone.PhoneTypeId = phone.PhoneTypeId;
						existingPhone.PhoneNumber = phone.PhoneNumber;
						existingPhone.Extension = phone.Extension;
					}

					responsePersonDto.Phones.Add(phone);
				}
			}

			if (personDto.Emails.Count > 0)
			{
				foreach (var email in personDto.Emails)
				{
					var existingEmail = await db.Emails
						.FirstOrDefaultAsync(a => a.EmailId == email.EmailId);
					if (existingEmail == null)
					{
						db.Emails.Add(email);
						await db.SaveChangesAsync();
						db.PersonEmailLookups.Add(new PersonEmailLookup()
						{
							PersonId = person.PersonId,
							EmailId = email.EmailId
						});
					}
					else
					{
						existingEmail.EmailTypeId = email.EmailId;
						existingEmail.EmailAddress = email.EmailAddress;
					}

					responsePersonDto.Emails.Add(email);
				}
			}

			responsePersonDto.Person = person;
			response.StatusCode = HttpStatusCode.OK;
			response.Success = true;
			response.Data = responsePersonDto;
			return Ok(response);
		}
		catch (Exception e)
		{
			response.ErrorMessages.Add(e.Message);
			response.Success = false;
			response.StatusCode = HttpStatusCode.InternalServerError;
			return StatusCode((int) HttpStatusCode.InternalServerError, response);
		}
	}
}