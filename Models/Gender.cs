using System.ComponentModel.DataAnnotations;

namespace CustomerServiceApi.Models;

public class Gender
{
	[Key]
	public int GenderId { get; set; }
	[Required]
	[MaxLength(50)]
	public string Description { get; set; }
	[Required]
	[MaxLength(50)]
	public string Name { get; set; }
}

