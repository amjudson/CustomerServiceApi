using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomerServiceApi.Models;

public class PersonEmailLookup
{
	[Key]
	public int PersonEmailLookupId { get; set; }
	[Required]
	public int PersonId { get; set; }
	[JsonIgnore]
	[ForeignKey("PersonId")]
	public Person Person { get; set; }
	[Required]
	public int EmailId { get; set; }
	[JsonIgnore]
	[ForeignKey("EmailId")]
	public Email Email { get; set; }
	[Required]
	public bool Primary { get; set; }
}

