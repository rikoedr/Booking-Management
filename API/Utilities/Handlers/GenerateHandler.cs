namespace API.Utilities.Handlers;

public class GenerateHandler
{
    public static string CreateNIK(string? latestNIK)
    {
        if(latestNIK is null)
        {
            return "111111";
        }

        int result = int.Parse(latestNIK) + 1;

        return result.ToString();
    }
}
