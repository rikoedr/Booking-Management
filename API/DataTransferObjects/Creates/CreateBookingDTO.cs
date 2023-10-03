using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DataTransferObjects.Creates;

public class CreateBookingDTO
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int Status { get; set; }

    public string Remarks { get; set; }

    public Guid RoomGuid { get; set; }

    public Guid EmployeeGuid { get; set; }

    public static implicit operator Booking(CreateBookingDTO createBookingDTO)
    {
        return new Booking
        {
            Guid = Guid.NewGuid(),
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            StartDate = createBookingDTO.StartDate,
            EndDate = createBookingDTO.EndDate,
            Status = createBookingDTO.Status,
            Remarks = createBookingDTO.Remarks,
            RoomGuid = createBookingDTO.RoomGuid,
            EmployeeGuid = createBookingDTO.EmployeeGuid,
        };
    }
}
