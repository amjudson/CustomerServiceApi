using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomerServiceApi.Models;

public class Employee
{
	[Key]
	public int EmployeeId { get; set; }
	[Required]
	public int PersonId { get; set; }
	[JsonIgnore]
	[ForeignKey("PersonId")]
	public Person Person { get; set; }
	[Required]
	public int EmployeeTypeId { get; set; }
	[JsonIgnore]
	[ForeignKey("EmployeeTypeId")]
	public EmployeeType EmployeeType { get; set; }
	[MaxLength(30)]
	public string Alias { get; set; }
}

