using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomerServiceApi.Models;

public class PersonPhoneLookup
{
	[Key]
	public int PersonPhoneLookupId { get; set; }
	[Required]
	public int PersonId { get; set; }
	[JsonIgnore]
	[ForeignKey("PersonId")]
	public Person Person { get; set; }
	[Required]
	public int PhoneId { get; set; }
	[JsonIgnore]
	[ForeignKey("PhoneId")]
	public Phone Phone { get; set; }
	[Required]
	public bool Primary { get; set; }
}

