using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceApi.Models;

public class PersonEmailLookup
{
	[Key]
	public int PersonEmailLookupId { get; set; }
	[Required]
	public int PersonId { get; set; }
	[ForeignKey("PersonId")]
	public Person Person { get; set; }
	[Required]
	public int EmailId { get; set; }
	[ForeignKey("EmailId")]
	public Email Email { get; set; }
	[Required]
	public bool Primary { get; set; }
}

