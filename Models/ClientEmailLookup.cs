using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomerServiceApi.Models;

public class ClientEmailLookup
{
	[Key]
	public int ClientEmailLookupId { get; set; }
	[Required]
	public Guid ClientId { get; set; }
	[JsonIgnore]
	[ForeignKey("ClientId")]
	public Client Client { get; set; }
	[Required]
	public int EmailId { get; set; }
	[JsonIgnore]
	[ForeignKey("EmailId")]
	public Email Email { get; set; }
}

