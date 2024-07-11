namespace CustomerServiceApi.Models.Dto;
public class RolesViewModel
{
	public ApplicationUser User { get; set; }
	public List<RoleSelection> RolesList { get; set; } = [];
}
