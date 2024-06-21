namespace CustomerServiceApi.Models.Dto;
public class PersonDto
{
	public Person Person { get; set; }
	public List<AddressDto> Addresses { get; set; }
	public List<Phone> Phones { get; set; }
	public List<Email> Emails { get; set; }
	public List<Appointment> Appointments { get; set; }
}
