namespace API.Utilities.Handlers;

/*
 * Response Error handler adalah class yang berfungsi sebagai format response API
 * berkategori Error atau gagal.
 */
public class ResponseErrorHandler
{
    public int Code { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public string? Error { get; set; }

}
