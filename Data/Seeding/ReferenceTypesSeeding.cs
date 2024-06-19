using CustomerServiceApi.Models;

namespace CustomerServiceApi.Data.Seeding;

public class ReferenceTypesSeeding
{
	public static List<AddressType> GetAddressTypes()
	{
		return
		[
			new AddressType
			{
				AddressTypeId = 1,
				Description = "Home",
				Name = "Home",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new AddressType
			{
				AddressTypeId = 2,
				Description = "Work",
				Name = "Work",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new AddressType
			{
				AddressTypeId = 3,
				Description = "Vacation",
				Name = "Vacation",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new AddressType
			{
				AddressTypeId = 4,
				Description = "Other",
				Name = "Other",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
		];
	}

	public static List<AppointmentType> GetAppointmentTypes()
	{
		return
		[
			new AppointmentType
			{
				AppointmentTypeId = 1,
				Description = "Business Meeting",
				Name = "Business Meeting",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
		];
	}

	public static List<EmailType> GetEmailTypes()
	{
		return
		[
			new EmailType
			{
				EmailTypeId = 1,
				Description = "Home",
				Name = "Home",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new EmailType
			{
				EmailTypeId = 2,
				Description = "Work",
				Name = "Work",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new EmailType
			{
				EmailTypeId = 3,
				Description = "Junk",
				Name = "Junk",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new EmailType
			{
				EmailTypeId = 4,
				Description = "Other",
				Name = "Other",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
		];
	}

	public static List<EmployeeType> GetEmployeeTypes()
	{
		return
		[
			new EmployeeType
			{
				EmployeeTypeId = 1,
				Description = "Not in a managerial position",
				Name = "Non-management",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new EmployeeType
			{
				EmployeeTypeId = 2,
				Description = "Managerial position",
				Name = "Management",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new EmployeeType
			{
				EmployeeTypeId = 3,
				Description = "Group leader position",
				Name = "Lead",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new EmployeeType
			{
				EmployeeTypeId = 4,
				Description = "General clerical position",
				Name = "Clerical",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
		];
	}

	public static List<Gender> GetGenders()
	{
		return
		[
			new Gender
			{
				GenderId = 1,
				Description = "Male Person",
				Name = "Male",
			},
			new Gender
			{
				GenderId = 2,
				Description = "Female Person",
				Name = "Female",
			},
			new Gender
			{
				GenderId = 3,
				Description = "Unknown",
				Name = "Other",
			},
		];
	}

	public static List<PersonType> GetPersonTypes()
	{
		return
		[
			new PersonType
			{
				PersonTypeId = 1,
				Description = "Customer",
				Name = "Customer",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new PersonType
			{
				PersonTypeId = 2,
				Description = "Employee",
				Name = "Employee",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new PersonType
			{
				PersonTypeId = 3,
				Description = "Guardian",
				Name = "Guardian",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new PersonType
			{
				PersonTypeId = 4,
				Description = "Mother",
				Name = "Mother",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new PersonType
			{
				PersonTypeId = 5,
				Description = "Father",
				Name = "Father",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new PersonType
			{
				PersonTypeId = 6,
				Description = "Sibling",
				Name = "Sibling",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new PersonType
			{
				PersonTypeId = 7,
				Description = "Brother",
				Name = "Brother",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new PersonType
			{
				PersonTypeId = 8,
				Description = "Sister",
				Name = "Sister",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
		];
	}

	public static List<PhoneType> GetPhoneTypes()
	{
		return
		[
			new PhoneType
			{
				PhoneTypeId = 1,
				Description = "Cell",
				Name = "Cell",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new PhoneType
			{
				PhoneTypeId = 2,
				Description = "Work",
				Name = "Work",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new PhoneType
			{
				PhoneTypeId = 3,
				Description = "Home",
				Name = "Home",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
			new PhoneType
			{
				PhoneTypeId = 4,
				Description = "Other",
				Name = "Other",
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
			},
		];
	}

	public static List<Race> GetRaces()
	{
		return
		[
			new Race
			{
				RaceId = 1,
				Description = "Caucasian ",
				Name = "Caucasian ",
			},
			new Race
			{
				RaceId = 2,
				Description = "African American ",
				Name = "African American",
			},
			new Race
			{
				RaceId = 3,
				Description = "Hispanic",
				Name = "Hispanic",
			},
			new Race
			{
				RaceId = 4,
				Description = "Asian",
				Name = "Asian",
			},
			new Race
			{
				RaceId = 5,
				Description = "Other",
				Name = "Other",
			},
		];
	}
}