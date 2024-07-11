﻿using System.Security.Claims;

namespace CustomerServiceApi.Data;
public static class ClaimStore
{
	public static readonly List<Claim> ClaimsList =
	[
		new Claim("Create", "Create"),
		new Claim("Edit", "Edit"),
		new Claim("Delete", "Delete"),
	];
}