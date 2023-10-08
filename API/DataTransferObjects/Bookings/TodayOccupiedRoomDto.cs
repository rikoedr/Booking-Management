namespace API.DataTransferObjects.Bookings;

public class TodayOccupiedRoomDto
{
    public Guid BookingGuid { get; set; }
    public string RoomName { get; set; }
    public int Status { get; set; }
    public int Floor { get; set; }
    public string BookedBy { get; set; }
}
