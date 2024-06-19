using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceApi.Models;

public class Employee
{
	[Key]
	public int EmployeeId { get; set; }
	[Required]
	public int PersonId { get; set; }
	[ForeignKey("PersonId")]
	public Person Person { get; set; }
	[Required]
	public int EmployeeTypeId { get; set; }
	[ForeignKey("EmployeeTypeId")]
	public EmployeeType EmployeeType { get; set; }
	[MaxLength(30)]
	public string Alias { get; set; }
}

