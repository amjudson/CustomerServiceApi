namespace CustomerServiceApi.Models.Dto;
public class ClaimsViewModel
{
	public ApplicationUserDto UserDto { get; set; }
	public List<ClaimSelection> ClaimsList { get; set; } = [];
}
