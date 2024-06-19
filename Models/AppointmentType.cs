using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceApi.Models;

public class AppointmentType
{
	[Key]
	public int AppointmentTypeId { get; set; }
	[Required]
	[MaxLength(50)]
	public string Name { get; set; }
	[MaxLength(512)]
	public string Description { get; set; }
	public Guid? ClientId { get; set; }
	[ForeignKey("ClientId")]
	public Client Client { get; set; }
}

