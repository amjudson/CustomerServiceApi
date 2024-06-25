using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomerServiceApi.Models;

public class ClientAddressLookup
{
	[Key]
	public int ClientAddressLookupId { get; set; }
	[Required]
	public Guid ClientId { get; set; }
	[JsonIgnore]
	[ForeignKey("ClientId")]
	public Client Client { get; set; }
	[Required]
	public int AddressId { get; set; }
	[JsonIgnore]
	[ForeignKey("AddressId")]
	public Address Address { get; set; }
}

