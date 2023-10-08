namespace API.DataTransferObjects.Bookings;

public class BookingDurationDTO
{
    public Guid RoomGuid { get; set; }
    public string RoomName { get; set; }
    public TimeSpan BookingLength { get; set; }
}
