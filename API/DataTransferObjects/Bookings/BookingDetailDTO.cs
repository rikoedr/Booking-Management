namespace API.DataTransferObjects.Bookings;

public class BookingDetailDTO
{
    public Guid Guid { get; set; }
    public string BookedNIK { get; set; }
    public string BookedBy { get; set; }
    public string RoomName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime Enddate { get; set; }
    public int Status { get; set; }
    public string Remarks { get; set; }
}
