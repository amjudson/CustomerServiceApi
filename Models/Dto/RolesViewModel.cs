﻿namespace CustomerServiceApi.Models.Dto;
public class RolesViewModel
{
	public ApplicationUserDto UserDto { get; set; }
	public List<RoleSelection> RolesList { get; set; } = [];
}
