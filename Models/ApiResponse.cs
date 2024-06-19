using System.Net;

namespace CustomerServiceApi.Models;
public class ApiResponse
{
	public ApiResponse()
	{
		ErrorMessages = new List<string>();
	}

	public HttpStatusCode StatusCode { get; set; }
	public bool Success { get; set; } = true;
	public List<string> ErrorMessages { get; set; }
	public object Result { get; set; }
}
