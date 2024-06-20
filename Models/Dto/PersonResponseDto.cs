namespace CustomerServiceApi.Models.Dto;
public class PersonResponseDto
{
	public Person Person { get; set; }
	public List<AddressResponseDto> Addresses { get; set; }
	public List<Phone> Phones { get; set; }
	public List<Email> Emails { get; set; }
	public List<Appointment> Appointments { get; set; }
}
