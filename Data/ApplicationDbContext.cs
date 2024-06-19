using CustomerServiceApi.Data.Seeding;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CustomerServiceApi.Models;

namespace CustomerServiceApi.Data;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
{
	public DbSet<ApplicationUser> ApplicationUsers { get; set; }
	public DbSet<Address> Addresses { get; set; }
	public DbSet<AddressType> AddressTypes { get; set; }
	public DbSet<Appointment> Appointments { get; set; }
	public DbSet<AppointmentType> AppointmentTypes { get; set; }
	public DbSet<Client> Clients { get; set; }
	public DbSet<ClientFeature> ClientFeatures { get; set; }
	public DbSet<ClientFeatureLookup> ClientFeatureLookups { get; set; }
	public DbSet<ClientType> ClientTypes { get; set; }
	public DbSet<Email> Emails { get; set; }
	public DbSet<EmailType> EmailTypes { get; set; }
	public DbSet<Employee> Employees { get; set; }
	public DbSet<EmployeeType> EmployeeTypes { get; set; }
	public DbSet<Gender> Genders { get; set; }
	public DbSet<Person> People { get; set; }
	public DbSet<PersonAddressLookup> PersonAddressLookups { get; set; }
	public DbSet<PersonEmailLookup> PersonEmailLookups { get; set; }
	public DbSet<PersonPhoneLookup> PersonPhoneLookups { get; set; }
	public DbSet<PersonType> PersonTypes { get; set; }
	public DbSet<Phone> Phones { get; set; }
	public DbSet<PhoneType> PhoneTypes { get; set; }
	public DbSet<Race> Races { get; set; }
	public DbSet<States> States { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<ClientType>().HasData(ClientSeeding.GetClientTypes());
 		modelBuilder.Entity<Client>().HasData(ClientSeeding.GetClients());
	  modelBuilder.Entity<AddressType>().HasData(ReferenceTypesSeeding.GetAddressTypes());
	  modelBuilder.Entity<AppointmentType>().HasData(
		  new AppointmentType
		  {
			  AppointmentTypeId = 1,
			  Description = "Business Meeting",
			  Name = "Business Meeting",
			  ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
		  });
	  modelBuilder.Entity<EmailType>().HasData(
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
		  });
	  modelBuilder.Entity<EmployeeType>().HasData(
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
		  });
	  modelBuilder.Entity<Gender>().HasData(
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
		  });
	  modelBuilder.Entity<PersonType>().HasData(
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
		  });
	  modelBuilder.Entity<PhoneType>().HasData(
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
		  });
	  modelBuilder.Entity<Race>().HasData(
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
		  });
	  modelBuilder.Entity<Person>().HasData(PeopleSeeding.GetPeople());
	}
}