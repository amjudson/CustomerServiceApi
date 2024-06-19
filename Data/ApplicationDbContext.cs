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
	  modelBuilder.Entity<AppointmentType>().HasData(ReferenceTypesSeeding.GetAppointmentTypes());
	  modelBuilder.Entity<EmailType>().HasData(ReferenceTypesSeeding.GetEmailTypes());
	  modelBuilder.Entity<EmployeeType>().HasData(ReferenceTypesSeeding.GetEmployeeTypes());
	  modelBuilder.Entity<Gender>().HasData(ReferenceTypesSeeding.GetGenders());
	  modelBuilder.Entity<PersonType>().HasData(ReferenceTypesSeeding.GetPersonTypes());
	  modelBuilder.Entity<PhoneType>().HasData(ReferenceTypesSeeding.GetPhoneTypes());
	  modelBuilder.Entity<Race>().HasData(ReferenceTypesSeeding.GetRaces());
	  modelBuilder.Entity<Person>().HasData(PeopleSeeding.GetPeople());
	}
}