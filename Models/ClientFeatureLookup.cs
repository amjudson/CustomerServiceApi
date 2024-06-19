using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceApi.Models;

public class ClientFeatureLookup
{
	[Key]
	public int ClientFeatureLookupId { get; set; }
	[Required]
	public Guid ClientId { get; set; }
	[ForeignKey("ClientId")]
	public Client Client { get; set; }
	[Required]
	public int ClientFeatureId { get; set; }
	[ForeignKey("ClientFeatureId")]
	public ClientFeature ClientFeature { get; set; }
}

