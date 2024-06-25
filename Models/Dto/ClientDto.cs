namespace CustomerServiceApi.Models.Dto;
public class ClientDto
{
	public Client Client { get; set; }
	public List<Address> Addresses { get; set; }
	public List<Phone> Phones { get; set; }
	public List<Email> Emails { get; set; }
}
