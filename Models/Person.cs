using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceApi.Models;

public class Person
{
	[Key]
	public int PersonId { get; set; }
	[Required]
	[MaxLength(50)]
	public string LastName { get; set; }
	[Required]
	[MaxLength(50)]
	public string FirstName { get; set; }
	[MaxLength(50)]
	public string MiddleName { get; set; }
	[MaxLength(10)]
	public string Suffix { get; set; }
	[MaxLength(10)]
	public string Prefix { get; set; }
	[Required]
	public int PersonTypeId { get; set; }
	[ForeignKey("PersonTypeId")]
	public PersonType PersonType { get; set; }
	[Required]
	[MaxLength(50)]
	public string Alias { get; set; }
	[Required]
	public int RaceId { get; set; }
	[ForeignKey("RaceId")]
	public Race Race { get; set; }
	[Required]
	public DateTime DateOfBirth { get; set; }
	[Required]
	public Guid ClientId { get; set; }
	[ForeignKey("ClientId")]
	public Client Client { get; set; }
	[Required]
	public int GenderId { get; set; }
	[ForeignKey("GenderId")]
	public Gender Gender { get; set; }
}

