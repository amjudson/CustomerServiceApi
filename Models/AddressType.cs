using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomerServiceApi.Models;

public class AddressType
{
	[Key]
	public int AddressTypeId { get; set; }
	[Required]
	[MaxLength(50)]
	public string Description { get; set; }
	public Guid? ClientId { get; set; }
	[JsonIgnore]
	[ForeignKey("ClientId")]
	public Client Client { get; set; }
	[Required]
	[MaxLength(50)]
	public string Name { get; set; }
}

