using API.Contracts;
using API.DataTransferObjects.Creates;
using API.DataTransferObjects;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Repositories;
using API.Utilities.Handlers;
using System.Net;
using API.Utilities.Responses;
using API.DataTransferObjects.Bookings;
using System.Security.Cryptography;

namespace API.Controllers;

/*
 * Room Controller adalah class untuk yang mengatur penerimaan request dan pengembalian response API.
 * Class ini terhubung dengan class Room Repository yang berfungsi untuk melakukan ORM.
 * Success dan Error Response di dalam controller ini di handle oleh ControllerBase dan Utility Class
 * terkait format Response API.
 */

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly RoomRepository _roomRepository;
    private readonly EmployeeRepository _employeeRepository;
    private readonly BookingRepository _bookingRepository;

    public RoomController(RoomRepository repository, EmployeeRepository employeeRepository, BookingRepository bookingRepository)
    {
        _roomRepository = repository;
        _employeeRepository = employeeRepository;
        _bookingRepository = bookingRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        // Get data collection from repository
        IEnumerable<Room> dataCollection = _roomRepository.GetAll();

        // Handling null data
        if (!dataCollection.Any())
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = Messages.DataNotFound

            });
        }

        // Return success response
        IEnumerable<RoomDTO> data = dataCollection.Select(item => (RoomDTO)item);
        ResponseOKHandler<IEnumerable<RoomDTO>> response = new ResponseOKHandler<IEnumerable<RoomDTO>>(data);

        return Ok(response);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        // Check if data available
        Room? data = _roomRepository.GetByGuid(guid);

        // Handling request if data is not found
        if (data is null)
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = Messages.DataNotFound
            });
        }

        // Return success response 
        RoomDTO response = (RoomDTO)data;

        return Ok(response);
    }

    [HttpPost]
    public IActionResult Create(CreateRoomDTO roomDTO)
    {
        try
        {
            // Create data from request paylod
            Room? result = _roomRepository.Create(roomDTO);

            // Return success response
            ResponseOKHandler<RoomDTO> response = new ResponseOKHandler<RoomDTO>((RoomDTO)result);

            return Ok(response);
        }
        catch (ExceptionHandler ex)
        {
            ResponseErrorHandler response = new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = Messages.FailedToCreateData,
                Error = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    [HttpPut]
    public IActionResult Update(RoomDTO roomDTO)
    {
        try
        {
            // Check if data available
            Room? entity = _roomRepository.GetByGuid(roomDTO.Guid);

            // Handling null data
            if (entity is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = Messages.DataNotFound
                });
            }

            // Update data
            Room toUpdate = roomDTO;
            toUpdate.CreatedDate = entity.CreatedDate;

            bool result = _roomRepository.Update(toUpdate);

            // Throw an exception if update failed
            if (!result)
            {
                throw new ExceptionHandler(Messages.ErrorOnUpdatingData);
            }

            // Return success response
            ResponseOKHandler<string> response = new ResponseOKHandler<string>(Messages.DataUpdated);

            return Ok(response);
        }
        catch (ExceptionHandler ex)
        {
            ResponseErrorHandler response = new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = Messages.FailedToCreateData,
                Error = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            // Check if data available
            Room? data = _roomRepository.GetByGuid(guid);

            // Handling null data
            if (data is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = Messages.DataNotFound
                });
            }

            // Delete data
            bool result = _roomRepository.Delete(data);

            // Throw an exception if update failed
            if (!result)
            {
                throw new ExceptionHandler(Messages.ErrorOnDeletingData);
            }

            // Return success response
            ResponseOKHandler<string> response = new ResponseOKHandler<string>(Messages.DataDeleted);

            return Ok(response);
        }
        catch (ExceptionHandler ex)
        {
            ResponseErrorHandler response = new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = Messages.FailedToCreateData,
                Error = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    [HttpGet]
    [Route("today/occupied")]
    public IActionResult GetTodayOccupiedRooms()
    {
        // Get data collection from repository
        IEnumerable<Booking> bookings = _bookingRepository.GetAll();

        // Handling empty colletion
        if (!bookings.Any())
        {
            return NotFound(ErrorResponses.DataNotFound("No rooms have been occupied yet"));
        }

        // Filter collection based on date (today)
        IEnumerable<Booking> todayBookings = bookings.Where(booking => booking.StartDate.Date == DateTime.Today.Date);

        // Handling empty today active room
        if (!todayBookings.Any())
        {
            return NotFound(ErrorResponses.DataNotFound("No rooms have been occupied today"));
        }

        // Return success response
        var employees = _employeeRepository.GetAll();
        var rooms = _roomRepository.GetAll();
        var occupiedRooms = from booking in todayBookings
                            join employee in employees on booking.EmployeeGuid equals employee.Guid
                            join room in rooms on booking.RoomGuid equals room.Guid
                            select new TodayOccupiedRoomDto
                            {
                                BookingGuid = booking.Guid,
                                RoomName = room.Name,
                                Status = booking.Status,
                                Floor = room.Floor,
                                BookedBy = string.Concat(employee.FirstName, " ", employee.LastName)
                            };

        return Ok(OkResponses.Success(occupiedRooms));
    }

    [HttpGet]
    [Route("today/available")]
    public IActionResult GetTodayAvalableRooms()
    {
        // Get all room & booking records
        var rooms = _roomRepository.GetAll();
        var bookings = _bookingRepository.GetAll();

        // Return all rooms
        if (!bookings.Any())
        {
            return Ok(OkResponses.Success((RoomDTO) rooms));
        }

        // Filter booking records by today
        var todayBookings = bookings.Where(booking => booking.StartDate.Date == DateTime.Today.Date);
        
        if (!todayBookings.Any())
        {
            return Ok(OkResponses.Success((RoomDTO)rooms));
        }

        var todayOccupiedRooms = from booking in todayBookings
                                 join room in rooms on booking.RoomGuid equals room.Guid
                                 select room;

        // Except all rooms and occupied rooms by guid
        Guid[] occupiedRoomGuids = todayOccupiedRooms.Select(item => item.Guid).ToArray();
        Guid[] roomGuids = rooms.Select(item => item.Guid).ToArray();
        var availableRoomGuids = roomGuids.Except(occupiedRoomGuids);

        // Get All Available Room by Guid
        var availableRooms = new List<RoomDTO>();
        foreach(Guid guid in availableRoomGuids)
        {
            var room = rooms.First(item => item.Guid == guid);
            availableRooms.Add((RoomDTO)room);
        }

        // Return success response
        return Ok(OkResponses.Success(availableRooms));
    }
}
