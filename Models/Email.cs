using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomerServiceApi.Models;

public class Email
{
	[Key]
	public int EmailId { get; set; }
	[Required]
	[MaxLength(1024)]
	public string EmailAddress { get; set; }
	[Required]
	public int EmailTypeId { get; set; }
	[JsonIgnore]
	[ForeignKey("EmailTypeId")]
	public EmailType EmailType { get; set; }
}

