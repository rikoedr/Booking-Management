using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.DataTransferObjects;
using API.DataTransferObjects.Creates;
using API.Utilities.Handlers;
using System.Net;
using API.Repositories;
using API.Utilities.Responses;
using Microsoft.AspNetCore.Server.IIS.Core;
using API.DataTransferObjects.Bookings;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

// BOOKING CONTROLLER IS A CLASS FOR SETTING UP BOOKING ENDPOINT.g

[ApiController]
[Route("api/booking")]
public class BookingController : ControllerBase
{
    private readonly BookingRepository _bookingRepository;
    private readonly EmployeeRepository _employeeRepository;
    private readonly RoomRepository _roomRepository;

    public BookingController(BookingRepository bookingRepository, EmployeeRepository employeeRepository, RoomRepository roomRepository)
    {
        _bookingRepository = bookingRepository;
        _employeeRepository = employeeRepository;
        _roomRepository = roomRepository;
    }

    // Endpoint for getting all booking data
    [HttpGet, Authorize(Roles = "admin")]
    public IActionResult GetAll()
    {
        // Get bookings data from repository
        var bookings = _bookingRepository.GetAll();

        // Handling empty bookings
        if (!bookings.Any())
        {
            return NotFound(ErrorResponses.DataNotFound());
        }

        // Return success response
        var bookingsDto = bookings.Select(item => (BookingDTO)item);

        return Ok(OkResponses.Success(bookingsDto));
    }

    // Endpoint for getting booking data by guid
    [HttpGet("{guid}"), Authorize(Roles = "admin")]
    public IActionResult GetByGuid(Guid guid)
    {
        // Check booking data availability by guid
        Booking? booking = _bookingRepository.GetByGuid(guid);

        // Handling null booking data
        if (booking is null)
        {
            return NotFound(ErrorResponses.DataNotFound());
        }

        // Return success response
        return Ok(OkResponses.Success((BookingDTO)booking));
    }

    // Endpoint for creating booking data
    [HttpPost, Authorize(Roles = "user")]
    public IActionResult Create(CreateBookingDTO request)
    {
        try
        {
            // Check employee and room data availability
            bool isEmployeeExist = _employeeRepository.IsAvailable(request.EmployeeGuid);
            bool isRoomExist = _roomRepository.IsAvailable(request.RoomGuid);

            // Handling employee or data not found
            if (!isEmployeeExist || !isEmployeeExist)
            {
                return NotFound(ErrorResponses.DataNotFound("Booking or employee data is not found"));
            }

            // Create booking data
            Booking? newBooking = _bookingRepository.Create(request);

            if(newBooking is null)
            {
                throw new ExceptionHandler(Messages.FailedToCreateBookingData);
            }

            // Return success response
            return Ok(OkResponses.Success((BookingDTO) newBooking));
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError, 
                ErrorResponses.CreateFailedCode500(ex.Message));
        }
    }

    // Endpoint for update booking data
    [HttpPut, Authorize("admin")]
    public IActionResult Update(BookingDTO request)
    {
        try
        {
            // Check data availability
            Booking? getBookingResult = _bookingRepository.GetByGuid(request.Guid);
            bool isEmployeeAvailable = _employeeRepository.IsAvailable(request.EmployeeGuid);
            bool isRoomAvailable = _roomRepository.IsAvailable(request.RoomGuid);

            // Handling unvailable booking data
            if (getBookingResult is null || !isEmployeeAvailable || !isRoomAvailable)
            {
                return NotFound(ErrorResponses.DataNotFound("Booking data not found"));
            }

            // Handling employee or data not found
            if (!isEmployeeAvailable || !isEmployeeAvailable)
            {
                return NotFound(ErrorResponses.DataNotFound("Booking or employee data is not found"));
            }

            // Update data
            Booking toUpdate = request;
            toUpdate.CreatedDate = getBookingResult.CreatedDate;

            bool updateBookingResult = _bookingRepository.Update(toUpdate);

            // Update failed handling
            if (!updateBookingResult)
            {
                throw new ExceptionHandler("Failed to update booking data");
            }

            // Return success response
            ResponseOKHandler<string> response = new ResponseOKHandler<string>(Messages.DataUpdated);

            return Ok(OkResponses.SuccessUpdate());
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError, 
                ErrorResponses.UpdateFailedCode500(ex.Message));
        }
    }

    // Endpoint for delete booking data
    [HttpDelete("{guid}"), Authorize(Roles = "manager")]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            // Check if data available
            Booking? getBookingResult = _bookingRepository.GetByGuid(guid);

            // Handling null data
            if (getBookingResult is null)
            {
                return NotFound(ErrorResponses.DataNotFound());
            }

            // Delete data
            bool isBookingDeletedResult = _bookingRepository.Delete(getBookingResult);

            // Throw an exception if update failed
            if (!isBookingDeletedResult)
            {
                throw new ExceptionHandler("Failed to delete booking data");
            }

            // Return success response
            ResponseOKHandler<string> response = new ResponseOKHandler<string>(Messages.DataDeleted);

            return Ok(response);
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError, 
                ErrorResponses.DeleteFailedCode500(ex.Message));
        }
    }

    // Endpoint for getting all booking data with detailed information
    [HttpGet, Authorize(Roles = "admin")]
    [Route("detail")]
    public IActionResult GetAllDetail()
    {
        // Get data collection from repository
        IEnumerable<Booking> getBookingsResult = _bookingRepository.GetAll();

        // Handling empty colletion
        if (!getBookingsResult.Any())
        {
            return NotFound(ErrorResponses.DataNotFound());
        }

        // Create booking detail dto with LINQ
        var employees = _employeeRepository.GetAll();
        var rooms = _roomRepository.GetAll();
        var details = from booking in getBookingsResult
                      join employee in employees on booking.EmployeeGuid equals employee.Guid
                      join room in rooms on booking.RoomGuid equals room.Guid
                      select new BookingDetailDTO
                      {
                          Guid = booking.Guid,
                          BookedNIK = employee.NIK,
                          BookedBy = string.Concat(employee.FirstName, " ", employee.LastName),
                          RoomName = room.Name,
                          StartDate = booking.StartDate,
                          Enddate = booking.EndDate,
                          Status = booking.Status,
                          Remarks = booking.Remarks
                      };

        // Return success response
        return Ok(OkResponses.Success(details));
    }

    // Endpoint for getting detailed booking data information by guid
    [HttpGet]
    [Route("details/{guid}")]
    public IActionResult GetDetailByGuid(Guid guid)
    {
        // Check booking data availability 
        Booking? booking = _bookingRepository.GetByGuid(guid);
        
        if(booking is null)
        {
            return NotFound(ErrorResponses.DataNotFound());
        }

        // Get employee and room data
        Employee? employee = _employeeRepository.GetByGuid(booking.EmployeeGuid);
        Room? room = _roomRepository.GetByGuid(booking.RoomGuid);

        // Create booking detail dto
        var bookingdetailDTO = new BookingDetailDTO
        {
            Guid = booking.Guid,
            BookedNIK = employee.NIK,
            BookedBy = string.Concat(employee.FirstName, " ", employee.LastName),
            RoomName = room.Name,
            StartDate = booking.StartDate,
            Enddate = booking.EndDate,
            Status = booking.Status,
            Remarks = booking.Remarks
        };

        // Return success response
        return Ok(OkResponses.Success(bookingdetailDTO));
    }

    // Endpoint for getting booking duration
    [HttpGet]
    [Route("duration")]
    public IActionResult GetDuration()
    {
        // Check booking data
        var bookings = _bookingRepository.GetAll();
        var rooms = _roomRepository.GetAll();

        // Handling empty colletion
        if (!bookings.Any())
        {
            return NotFound(ErrorResponses.DataNotFound());
        }

        // Join 
        var bookingDuration = from booking in bookings
                              join room in rooms on booking.RoomGuid equals room.Guid
                              select new BookingDurationDTO
                              {
                                  RoomGuid = room.Guid,
                                  RoomName = room.Name,
                                  BookingLength = GenerateHandler.BookingDuration(booking.StartDate, booking.EndDate)
                              };

        // Return success
        return Ok(OkResponses.Success(bookingDuration));
    }
}
