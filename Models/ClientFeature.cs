using System.ComponentModel.DataAnnotations;

namespace CustomerServiceApi.Models;

public class ClientFeature
{
	[Key]
	public int ClientFeatureId { get; set; }
	[MaxLength(512)]
	public string DisplayName { get; set; }
	[MaxLength(1)]
	public string FeatureValue { get; set; }
	[Required]
	public DateTime CreatedDate { get; set; }
	[Required]
	public DateTime ModifiedDate { get; set; }
	[Required]
	[MaxLength(512)]
	public string EnteredBy { get; set; }
}

