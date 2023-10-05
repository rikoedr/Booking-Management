namespace API.Utilities.Handlers;

/*
 * Generate Handler adalah class utilitas yang berfungsi untuk melakukan generate seperti
 * NIK dan lain-lain sesuai kebutuhan project.
 */

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

    public static int CreateOTP()
    {
        return new Random().Next(100000, 1000000); 
    }
}
