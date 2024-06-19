using System.ComponentModel.DataAnnotations;

namespace CustomerServiceApi.Models;

public class ClientType
{
	[Key]
	public int ClientTypeId { get; set; }
	[MaxLength(512)]
	public string Description { get; set; }
	[Required]
	[MaxLength(50)]
	public string Name { get; set; }
}

