using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceApi.Models;

public class Appointment
{
	[Key]
	public int AppointmentId { get; set; }
	[Required]
	[MaxLength(50)]
	public string Name { get; set; }
	[MaxLength(512)]
	public string Description { get; set; }
	[Required]
	public int AppointmentTypeId { get; set; }
	[ForeignKey("AppointmentTypeId")]
	public AppointmentType AppointmentType { get; set; }
	[Required]
	public DateTime StartDateTime { get; set; }
	[Required]
	public DateTime EndDateTime { get; set; }
	[Required]
	public bool AllDay { get; set; }
}

