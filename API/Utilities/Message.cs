namespace API.Utilities;

/*
 * Message Class adalah kumpulan static string untuk message response API.
 */

public class Message
{
    // Ok Message
    public static readonly string DataDeleted = "Data Deleted";
    public static readonly string DataUpdated = "Data Updated";

    // Failed Message
    public static readonly string DataNotFound = "Data Not Found";
    public static readonly string FailedToDeleteData = "Failed to Delete Data";
    public static readonly string FailedToUpdateData = "Failed to Update Data";
    public static readonly string FailedToCreateData = "Failed to Create Data";

    // Error Message
    public static readonly string ErrorOnUpdatingData = "Error on Updating Data!";
    public static readonly string ErrorOnDeletingData = "Error on Deleting Data!";

    // Validation Message
    public static readonly string CanNotBeEmpty = "Can Not Be Empty";
    public static readonly string MaximumCharLength100 = "Maximum character length is 100";
    public static readonly string CannotLessThanEqual0 = "The value cannot be less than equal to 0";
}
