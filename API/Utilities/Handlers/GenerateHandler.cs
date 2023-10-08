namespace API.Utilities.Handlers;

/*
 * Generate Handler adalah class utilitas yang berfungsi untuk melakukan generate seperti
 * NIK dan lain-lain sesuai kebutuhan project.
 */

public class GenerateHandler
{
    public static string NIK(string? latestNIK)
    {
        if(latestNIK is null) return "111111";

        int result = int.Parse(latestNIK) + 1;

        return result.ToString();
    }

    public static int OTP()
    {
        return new Random().Next(100000, 1000000); 
    }

    // Static method for generating booking duration based on start and end datetime
    public static TimeSpan BookingDuration(DateTime start, DateTime end)
    {
        //Return zero for start = saturday and end = sunday
        if(start.DayOfWeek == DayOfWeek.Saturday && end.DayOfWeek == DayOfWeek.Sunday)
        {
            return new TimeSpan();
        }

        // Calculate time span from star to end date 
        TimeSpan timeSpan = end - start;
        int totalDays = (int)timeSpan.TotalDays;

        // Count saturday and sunday in booking time range
        int count = (int) start.DayOfWeek;
        int saturday = 0;
        int sunday = 0;

        for (int i = 0; i < totalDays; i++)
        {
            if (count == 6) //saturday = 6
            {
                saturday += 1;
                count = -1; //reset counter
            }
            if (count == 0) //sunday = 0
            {
                sunday += 1;
            }
            count += 1;
        }

        // Subtract timespan with exluded duration (saturday and sunday)
        TimeSpan excludedDuration = TimeSpan.FromHours((saturday + sunday) * 24);
        TimeSpan finalDuration = timeSpan.Subtract(excludedDuration);

        return finalDuration;
    }


}
