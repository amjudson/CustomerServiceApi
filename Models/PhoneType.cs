using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceApi.Models;

public class PhoneType
{
	[Key]
	public int PhoneTypeId { get; set; }
	[Required]
	[MaxLength(50)]
	public string Description { get; set; }
	public Guid? ClientId { get; set; }
	[ForeignKey("ClientId")]
	public Client Client { get; set; }
	[Required]
	[MaxLength(50)]
	public string Name { get; set; }
}

