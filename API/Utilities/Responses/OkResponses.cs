using API.Utilities.Handlers;

namespace API.Utilities.Responses;

public class OkResponses
{
    // Succes Response Collection
    public static ResponseOKHandler<string> Success()
    {
        var response = new ResponseOKHandler<string>("Success");

        return response;
    }

    public static ResponseOKHandler<TEntity> Success<TEntity>(TEntity entity)
    {
        var response = new ResponseOKHandler<TEntity>(entity);

        return response;
    }
    public static ResponseOKHandler<TEntity> Success<TEntity>(string message, TEntity entity)
    {
        var response = new ResponseOKHandler<TEntity>(message, entity);

        return response;
    }


    public static ResponseOKHandler<string> SuccessUpdate()
    {
        var response = new ResponseOKHandler<string>(Messages.DataUpdated);

        return response;
    }

    public static ResponseOKHandler<string> SuccessDelete()
    {
        var response = new ResponseOKHandler<string>(Messages.DataDeleted);

        return response;
    }
}
