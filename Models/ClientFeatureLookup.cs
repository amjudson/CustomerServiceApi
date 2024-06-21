using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomerServiceApi.Models;

public class ClientFeatureLookup
{
	[Key]
	public int ClientFeatureLookupId { get; set; }
	[Required]
	public Guid ClientId { get; set; }
	[JsonIgnore]
	[ForeignKey("ClientId")]
	public Client Client { get; set; }
	[Required]
	public int ClientFeatureId { get; set; }
	[JsonIgnore]
	[ForeignKey("ClientFeatureId")]
	public ClientFeature ClientFeature { get; set; }
}

