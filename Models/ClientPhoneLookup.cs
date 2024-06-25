using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomerServiceApi.Models;

public class ClientPhoneLookup
{
	[Key]
	public int ClientPhoneLookupId { get; set; }
	[Required]
	public Guid ClientId { get; set; }
	[JsonIgnore]
	[ForeignKey("ClientId")]
	public Client Client { get; set; }
	[Required]
	public int PhoneId { get; set; }
	[JsonIgnore]
	[ForeignKey("PhoneId")]
	public Phone Phone { get; set; }
}

