namespace API.Utilities;

/*
 * Message Class adalah kumpulan static string untuk message response API.
 */

public class Message
{
    /*
     * GENERAL MESSAGES
     * Kumpulan static string berisi pesan-pesan umum dalam response API.
     */

    // General Ok Message
    public static readonly string DataDeleted = "Data Deleted";
    public static readonly string DataUpdated = "Data Updated";

    // General Failed Message
    public static readonly string DataNotFound = "Data Not Found";
    public static readonly string FailedToDeleteData = "Failed to Delete Data";
    public static readonly string FailedToUpdateData = "Failed to Update Data";
    public static readonly string FailedToCreateData = "Failed to Create Data";

    // General Error Message
    public static readonly string ErrorOnUpdatingData = "Error on Updating Data!";
    public static readonly string ErrorOnDeletingData = "Error on Deleting Data!";

    // General Validation Message
    public static readonly string CanNotBeEmpty = "This field can not be empty";
    public static readonly string MaximumCharLength100 = "Maximum character length is 100";
    public static readonly string CannotLessThanEqual0 = "The value cannot be less than equal to 0";
    

    /*
     * FIELD MESSAGES
     * Kumpulan pesan-pesan terkait field yang digunakan untuk reponse validasi dalam API.
     */

    // OTP Messages
    public static readonly string EmptyOTP = "OTP : This field can not be empty";
    public static readonly string InvalidOTPFormat = "OTP must be a 6-digit numeric code";
    public static readonly string InvalidOTPCode = "Invalid OTP Code";
    public static readonly string OTPCodeAlreadyUsed = "OTP Code has already been used";
    public static readonly string OTPCodeHasExpired = "OTP Code has expired";
    public static readonly string OTPCodeHasSent = "OTP code has sent to your email";

    // Email Messages
    public static readonly string EmptyEmail = "Email : This field can not be empty";
    public static readonly string InvalidEmailFormat = "Invalid email format";
    public static readonly string EmailAlreadyRegistered = "Email already registered";

    // Password Messages
    public static readonly string EmptyPassword = "Password : This field can not be empty";
    public static readonly string PasswordDoNotMatch = "New password and confirmation do not match";
    public static readonly string PasswordMinimumCharacter = "Password must have at least 6 characters";


    // Date Messages
    public static readonly string BirthDateEmpty = "Birth Date : This field can not be empty";
    public static readonly string HiringDateEmpty = "Hiring Date : This field can not be empty";
    public static readonly string BirthDateLessThanNow = "Birth Date must less than present time";

    // Bio Messages
    public static readonly string FirstNameEmpty = "First Name : This field can not be empty";
    public static readonly string GenderEmpty = "Gender : This field can not be empty";
    public static readonly string PhoneNumberEmpty = "Phone Number : This field can not be empty";
    public static readonly string MajorEmpty = "Major : This field can not be empty";
    public static readonly string DegreeEmpty = "Degree : This field can not be empty";
    public static readonly string GPAEmpty = "GPA : This field can not be empty";
    public static readonly string UniversityCodeEmpty = "University Code : This field can not be empty";
    public static readonly string UniversityNameEmpty = "University Name : This field can not be empty";

    
    /*
     * END POINT MESSAGES
     * Kumpulan message yang dibagi berdasarkan endpoint.
     */

    // Login Endpoint Messages
    public static readonly string LoginFailed = "Login Failed";
    public static readonly string AccountPasswordInvalid = "Account or Password is invalid";
}
