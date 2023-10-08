DateTime startDate = new DateTime(2023, 10, 5, 15, 30, 0);
DateTime endDate = new DateTime(2023, 10, 6, 15, 30, 0);
DateTime startDate2 = new DateTime(2023, 10, 7, 10, 0, 0);
DateTime endDate2 = new DateTime(2023, 10, 8, 18, 45, 0);

// Calculate time span from star to end date 
TimeSpan timeSpan = endDate2 - startDate2;
int totalDays = (int)timeSpan.TotalDays;

// Count saturday and sunday in booking time range
int count = (int) startDate2.DayOfWeek;
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

TimeSpan excludedDuration = TimeSpan.FromHours((saturday + sunday) * 24);
TimeSpan finalDuration = timeSpan.Subtract(excludedDuration);

Console.WriteLine(new TimeSpan());