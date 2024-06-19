using System.ComponentModel.DataAnnotations;

namespace CustomerServiceApi.Models;

public class States
{
	[Key]
	[Required]
	public int StateId { get; set; }
	[Required]
	[MaxLength(50)]
	public string Abbreviation { get; set; }
	[Required]
	[MaxLength(512)]
	public string Name { get; set; }
}

