using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceApi.Models;

public class Phone
{
	[Key]
	public int PhoneId { get; set; }
	[Required]
	[MaxLength(20)]
	public string PhoneNumber { get; set; }
	[MaxLength(10)]
	public string Extension { get; set; }
	[Required]
	public int PhoneTypeId { get; set; }
	[ForeignKey("PhoneTypeId")]
	public PhoneType PhoneType { get; set; }
}

