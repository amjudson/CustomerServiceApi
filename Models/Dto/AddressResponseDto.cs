namespace CustomerServiceApi.Models.Dto;
public class AddressResponseDto
{
	public int AddressId { get; set; }
	public string AddressLine1 { get; set; }
	public string AddressLine2 { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public string Zip { get; set; }
	public int AddressTypeId { get; set; }
	public string AddressType { get; set; }
}
