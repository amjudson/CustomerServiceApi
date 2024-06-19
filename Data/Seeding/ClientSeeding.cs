using CustomerServiceApi.Models;

namespace CustomerServiceApi.Data.Seeding;

public class ClientSeeding
{
	public static List<ClientType> GetClientTypes()
	{
		return
		[
			new ClientType
			{
				ClientTypeId = 1,
				Description = "Resort",
				Name = "Resort",
			},
			new ClientType
			{
				ClientTypeId = 2,
				Description = "Day Care",
				Name = "Day Care",
			},
			new ClientType
			{
				ClientTypeId = 3,
				Description = "School",
				Name = "School",
			},
			new ClientType
			{
				ClientTypeId = 4,
				Description = "Hotel",
				Name = "Hotel",
			},
			new ClientType
			{
				ClientTypeId = 5,
				Description = "Motel",
				Name = "Motel",
			},
			new ClientType
			{
				ClientTypeId = 6,
				Description = "Camp Ground",
				Name = "Camp Ground",
			},
			new ClientType
			{
				ClientTypeId = 7,
				Description = "Lawncare",
				Name = "Lawncare",
			},
		];
	}

	public static List<Client> GetClients()
	{
		return
		[
			new Client
			{
				ClientId = new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
				ClientName = "default-client",
				Active = false,
				ClientTypeId = 6,
				CreatedDate = DateTime.SpecifyKind(DateTime.Now.ToUniversalTime(), DateTimeKind.Utc),
				EnteredBy = "system",
			},
			new Client
			{
				ClientId = new Guid("55136886-37df-4188-b5ea-0e74958e627c"),
				ClientName = "Awesome Resort",
				Active = true,
				CreatedDate = DateTime.SpecifyKind(DateTime.Now.ToUniversalTime().ToUniversalTime(), DateTimeKind.Utc),
				EnteredBy = "system",
				ClientTypeId = 2,
			},
			new Client
			{
				ClientId = new Guid("4cc3d613-57ff-4fc8-8d7d-b908678769d2"),
				ClientName = "Wa Wa Campgounds",
				Active = true,
				CreatedDate = DateTime.SpecifyKind(DateTime.Now.ToUniversalTime(), DateTimeKind.Utc),
				EnteredBy = "system",
				ClientTypeId = 6,
			},
			new Client
			{
				ClientId = new Guid("53f8d078-11a1-4089-9ab3-39dbf72425d9"),
				ClientName = "Little Ones Daycare",
				Active = true,
				CreatedDate = DateTime.SpecifyKind(DateTime.Now.ToUniversalTime(), DateTimeKind.Utc),
				EnteredBy = "system",
				ClientTypeId = 2,
			},
			new Client
			{
				ClientId = new Guid("11270c83-4a0c-40f9-bbc5-6b6894d9d95d"),
				ClientName = "Best Lawncare",
				Active = true,
				CreatedDate = DateTime.SpecifyKind(DateTime.Now.ToUniversalTime(), DateTimeKind.Utc),
				EnteredBy = "system",
				ClientTypeId = 7,
			},
			new Client
			{
				ClientId = new Guid("50cf2794-76c4-4f35-8692-27bfbbfffee7"),
				ClientName = "Down The Road Motel",
				Active = false,
				CreatedDate = DateTime.SpecifyKind(DateTime.Now.ToUniversalTime(), DateTimeKind.Utc),
				EnteredBy = "system",
				ClientTypeId = 5,
			},
			new Client
			{
				ClientId = new Guid("9bfbf657-4ede-4e6c-94fe-ed5c1764af28"),
				ClientName = "Testing of The Person Tracking",
				Active = true,
				CreatedDate = DateTime.SpecifyKind(DateTime.Now.ToUniversalTime(), DateTimeKind.Utc),
				EnteredBy = "system",
				ClientTypeId = 1,
			},
		];
	}
}