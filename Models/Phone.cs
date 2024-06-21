using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
	[JsonIgnore]
	[ForeignKey("PhoneTypeId")]
	public PhoneType PhoneType { get; set; }
}

