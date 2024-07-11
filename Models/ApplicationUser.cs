using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceApi.Models;
public class ApplicationUser : IdentityUser
{
	[MaxLength(50)]
	public string FirstName { get; set; }
	[MaxLength(50)]
	public string LastName { get; set; }

	[NotMapped]
	public string RoleId { get; set; }
	[NotMapped]
	public List<string> Roles { get; set; }
	[NotMapped]
	public List<string> UserClaims { get; set; }
}
