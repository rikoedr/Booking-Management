namespace API.Utilities.Handlers;

public class GenderHandler
{
    public static string toString(int gender)
    {
        return gender == 0 ? "Male" : "Female"; 
    }
}
