using System.ComponentModel.DataAnnotations;

namespace CustomerServiceApi.Models;

public class Race
{
	[Key]
	public int RaceId { get; set; }
	[Required]
	[MaxLength(50)]
	public string Description { get; set; }
	[Required]
	[MaxLength(50)]
	public string Name { get; set; }
}

