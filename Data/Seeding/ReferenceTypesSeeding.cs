using CustomerServiceApi.Models;

namespace CustomerServiceApi.Data.Seeding;
public class ReferenceTypesSeeding
{
	public static List<AddressType> GetAddressTypes()
	{
		return
			[
				new AddressType
				{
					AddressTypeId = 1,
					Description = "Home",
					Name = "Home",
					ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
				},
				new AddressType
				{
					AddressTypeId = 2,
					Description = "Work",
					Name = "Work",
					ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
				},
				new AddressType
				{
					AddressTypeId = 3,
					Description = "Vacation",
					Name = "Vacation",
					ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
				},
				new AddressType
				{
					AddressTypeId = 4,
					Description = "Other",
					Name = "Other",
					ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
				},
			];
	}
}
