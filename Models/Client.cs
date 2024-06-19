using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceApi.Models;

public class Client
{
	[Key]
	public Guid ClientId { get; set; }
	[Required]
	[MaxLength(512)]
	public string ClientName { get; set; }
	[Required]
	public bool Active { get; set; }
	[Required]
	public DateTime CreatedDate { get; set; }
	[Required]
	[MaxLength(512)]
	public string EnteredBy { get; set; }
	[Required]
	public int ClientTypeId { get; set; }
	[ForeignKey("ClientTypeId")]
	public ClientType ClientType { get; set; }
}

