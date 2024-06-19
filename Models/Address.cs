using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceApi.Models;

public class Address
{
	[Key]
	public int AddressId { get; set; }
	[Required]
	[MaxLength(255)]
	public string AddressLine1 { get; set; }
	[MaxLength(255)]
	public string AddressLine2 { get; set; }
	[Required]
	[MaxLength(50)]
	public string City { get; set; }
	[Required]
	public int StateId { get; set; }
	[ForeignKey("StateId")]
	public States State { get; set; }
	[Required]
	[MaxLength(20)]
	public string Zip { get; set; }
	[Required]
	public int AddressTypeId { get; set; }
	[ForeignKey("AddressTypeId")]
	public AddressType AddressType { get; set; }
}

