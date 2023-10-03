using API.DataTransferObjects.Creates;
using API.Models;

namespace API.DataTransferObjects;

public class BookingDTO
{
    public Guid Guid { get; set; }
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int Status { get; set; }

    public string Remarks { get; set; }

    public Guid RoomGuid { get; set; }

    public Guid EmployeeGuid { get; set; }

    public static explicit operator BookingDTO(Booking booking)
    {
        return new BookingDTO
        {
            Guid = booking.Guid,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            Status = booking.Status,
            Remarks = booking.Remarks,
            RoomGuid = booking.RoomGuid,
            EmployeeGuid = booking.EmployeeGuid,
        };
    }

    public static implicit operator Booking(BookingDTO bookingDTO)
    {
        return new Booking
        {
            Guid = bookingDTO.Guid,
            ModifiedDate = DateTime.Now,
            StartDate = bookingDTO.StartDate,
            EndDate = bookingDTO.EndDate,
            Status = bookingDTO.Status,
            Remarks = bookingDTO.Remarks,
            RoomGuid = bookingDTO.RoomGuid,
            EmployeeGuid = bookingDTO.EmployeeGuid,
        };
    }
}
