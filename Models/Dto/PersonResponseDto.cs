namespace CustomerServiceApi.Models.Dto;
public class PersonResponseDto
{
	public Person Person { get; set; }
	public IEnumerable<Address> Addresses { get; set; }
	public IEnumerable<Phone> Phones { get; set; }
	public IEnumerable<Email> Emails { get; set; }
	public IEnumerable<Appointment> Appointments { get; set; }
}
