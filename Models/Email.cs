using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
	[ForeignKey("EmailTypeId")]
	public EmailType EmailType { get; set; }
}

