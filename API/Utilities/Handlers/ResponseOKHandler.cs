using System.Net;

namespace API.Utilities.Handlers;

/*
 * Response OK Handler adalah class yang berisi format pengaturan response API khusus
 * response berkategori Sukses.
 */

public class ResponseOKHandler<TEntity>
{
    public int Code { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public TEntity? Data { get; set; }

    public ResponseOKHandler()
    {
        
    }

    // Format response sukses beserta data
    public ResponseOKHandler(TEntity? data)
    {
        Code = StatusCodes.Status200OK;
        Status = HttpStatusCode.OK.ToString();
        Message = "Success to Retrieve Data";
        Data = data;
    }

    // Format response sukses tanpa data
    public ResponseOKHandler(string message)
    {
        Code = StatusCodes.Status200OK;
        Status = HttpStatusCode.OK.ToString();
        Message = message;
    }

    // Format response sukses dengan custom message dan data
    public ResponseOKHandler(string message, TEntity? data)
    {
        Code = StatusCodes.Status200OK;
        Status = HttpStatusCode.OK.ToString();
        Message = message;
        Data = data;
    }

}
