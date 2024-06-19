using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceApi.Models;

public class PersonPhoneLookup
{
	[Key]
	public int PersonPhoneLookupId { get; set; }
	[Required]
	public int PersonId { get; set; }
	[ForeignKey("PersonId")]
	public Person Person { get; set; }
	[Required]
	public int PhoneId { get; set; }
	[ForeignKey("PhoneId")]
	public Phone Phone { get; set; }
	[Required]
	public bool Primary { get; set; }
}

