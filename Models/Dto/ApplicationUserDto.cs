namespace CustomerServiceApi.Models.Dto;
public class ApplicationUserDto
{
	public string Id { get; set; }
	public string UserName { get; set; }
	public string Email { get; set; }
	public string PhoneNumber { get; set; }
	public string LastName { get; set; }
	public string FirstName { get; set; }
	public int AccessFailedCount { get; set; }
	public bool LockoutEnabled { get; set; }
	public DateTimeOffset? LockoutEnd { get; set; }
	public List<string> Roles { get; set; }
	public List<string> UserClaims { get; set; }
}
