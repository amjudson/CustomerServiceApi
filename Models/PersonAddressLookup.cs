using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomerServiceApi.Models;

public class PersonAddressLookup
{
	[Key]
	public int PersonAddressLookupId { get; set; }
	[Required]
	public int PersonId { get; set; }
	[JsonIgnore]
	[ForeignKey("PersonId")]
	public Person Person { get; set; }
	[Required]
	public int AddressId { get; set; }
	[JsonIgnore]
	[ForeignKey("AddressId")]
	public Address Address { get; set; }
	[Required]
	public bool Primary { get; set; }
}

