using API.Contracts;
using API.DataTransferObjects;
using API.DataTransferObjects.Creates;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

/*
 * University Controller adalah class untuk yang mengatur penerimaan request dan pengembalian response API.
 * Class ini terhubung dengan class University Repository yang berfungsi untuk melakukan ORM.
 * Success dan Error Response di dalam controller ini di handle oleh ControllerBase dan Utility Class
 * terkait format Response API.
 */

[ApiController]
[Route("api/[controller]")]
public class UniversityController : ControllerBase
{
    private readonly UniversityRepository _repository;

    public UniversityController(UniversityRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        // Get data collection from repository
        IEnumerable<University> dataCollection = _repository.GetAll();

        // Handling null data
        if (!dataCollection.Any())
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = Message.DataNotFound

            });
        }

        // Return success response
        IEnumerable<UniversityDTO> data = dataCollection.Select(item => (UniversityDTO)item);
        ResponseOKHandler<IEnumerable<UniversityDTO>> response = new ResponseOKHandler<IEnumerable<UniversityDTO>>(data);

        return Ok(response);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        // Check if data available
        University? data = _repository.GetByGuid(guid);

        // Handling request if data is not found
        if (data is null)
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = Message.DataNotFound
            });
        }

        // Return success response 
        UniversityDTO response = (UniversityDTO)data;

        return Ok(response);
    }

    [HttpPost]
    public IActionResult Create(CreateUniversityDTO universityDTO)
    {
        try
        {
            // Create data from request paylod
            University? result = _repository.Create(universityDTO);

            // Return success response
            ResponseOKHandler<UniversityDTO> response = new ResponseOKHandler<UniversityDTO>((UniversityDTO)result);

            return Ok(response);
        }
        catch(ExceptionHandler ex)
        {
            ResponseErrorHandler response = new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = Message.FailedToCreateData,
                Error = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    [HttpPut]
    public IActionResult Update(UniversityDTO universityDTO)
    {
        try
        {
            // Check if data available
            University? entity = _repository.GetByGuid(universityDTO.Guid);

            // Handling null data
            if (entity is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = Message.DataNotFound
                });
            }

            // Update data
            University toUpdate = universityDTO;
            toUpdate.CreatedDate = entity.CreatedDate;

            bool result = _repository.Update(toUpdate);

            // Throw an exception if update failed
            if (!result)
            {
                throw new ExceptionHandler(Message.ErrorOnUpdatingData);
            }

            // Return success response
            ResponseOKHandler<string> response = new ResponseOKHandler<string>(Message.DataUpdated);

            return Ok(response);
        }
        catch(ExceptionHandler ex)
        {
            ResponseErrorHandler response = new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = Message.FailedToCreateData,
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
            University? data = _repository.GetByGuid(guid);

            // Handling null data
            if (data is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = Message.DataNotFound
                });
            }

            // Delete data
            bool result = _repository.Delete(data);

            // Throw an exception if update failed
            if (!result)
            {
                throw new ExceptionHandler(Message.ErrorOnDeletingData);
            }

            // Return success response
            ResponseOKHandler<string> response = new ResponseOKHandler<string>(Message.DataDeleted);\

            return Ok(response);
        }
        catch(ExceptionHandler ex)
        {
            ResponseErrorHandler response = new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = Message.FailedToCreateData,
                Error = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
