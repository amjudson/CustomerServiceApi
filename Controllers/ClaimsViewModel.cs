using CustomerServiceApi.Models;
using CustomerServiceApi.Models.Dto;

namespace CustomerServiceApi.Controllers;
public class ClaimsViewModel
{
	public ApplicationUser User { get; set; }
	public List<ClaimSelection> ClaimsList { get; set; } = [];
}
