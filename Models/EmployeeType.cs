using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceApi.Models;

public class EmployeeType
{
	[Key]
	public int EmployeeTypeId { get; set; }
	[Required]
	[MaxLength(150)]
	public string Description { get; set; }
	[Required]
	[MaxLength(50)]
	public string Name { get; set; }
	public Guid? ClientId { get; set; }
	[ForeignKey("ClientId")]
	public Client Client { get; set; }
}

