using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace API.Utilities.Responses;

public class ErrorResponses 
{
    // Error Response Collection
    public static ResponseErrorHandler DataNotFound()
    {
        var response = new ResponseErrorHandler
        {
            Code = StatusCodes.Status404NotFound,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = Messages.DataNotFound
        };

        return response;
    }
    public static ResponseErrorHandler DataNotFound(string message)
    {
        var response = new ResponseErrorHandler
        {
            Code = StatusCodes.Status404NotFound,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = message
        };

        return response;
    }

    public static ResponseErrorHandler InternalServerError(string exception)
    {
        ResponseErrorHandler response = new ResponseErrorHandler()
        {
            Code = StatusCodes.Status500InternalServerError,
            Status = HttpStatusCode.InternalServerError.ToString(),
            Message = "Failed to do operation",
            Error = exception
        };

        return response;
    }

    public static ResponseErrorHandler InternalServerError(string message, string exception)
    {
        ResponseErrorHandler response = new ResponseErrorHandler()
        {
            Code = StatusCodes.Status500InternalServerError,
            Status = HttpStatusCode.InternalServerError.ToString(),
            Message = Messages.FailedToCreateData,
            Error = exception
        };

        return response;
    }

    public static ResponseErrorHandler UpdateFailedCode500(string exception)
    {
        ResponseErrorHandler response = new ResponseErrorHandler()
        {
            Code = StatusCodes.Status500InternalServerError,
            Status = HttpStatusCode.InternalServerError.ToString(),
            Message = Messages.FailedToUpdateData,
            Error = exception
        };

        return response;
    }
    public static ResponseErrorHandler DeleteFailedCode500(string exception)
    {
        ResponseErrorHandler response = new ResponseErrorHandler()
        {
            Code = StatusCodes.Status500InternalServerError,
            Status = HttpStatusCode.InternalServerError.ToString(),
            Message = Messages.FailedToDeleteData,
            Error = exception
        };

        return response;
    }

    public static ResponseErrorHandler CreateFailedCode500(string exception)
    {
        ResponseErrorHandler response = new ResponseErrorHandler()
        {
            Code = StatusCodes.Status500InternalServerError,
            Status = HttpStatusCode.InternalServerError.ToString(),
            Message = Messages.FailedToCreateData,
            Error = exception
        };

        return response;
    }




    public static ResponseErrorHandler AccountPasswordInvalid()
    {
        ResponseErrorHandler response = new ResponseErrorHandler
        {
            Code = StatusCodes.Status401Unauthorized,
            Status = HttpStatusCode.Unauthorized.ToString(),
            Message = Messages.AccountPasswordInvalid
        };

        return response;
    }

    public static ResponseErrorHandler DataConflict(string message)
    {
        ResponseErrorHandler response = new ResponseErrorHandler
        {
            Code = StatusCodes.Status401Unauthorized,
            Status = HttpStatusCode.Unauthorized.ToString(),
            Message = message
        };

        return response;
    }

    


}
