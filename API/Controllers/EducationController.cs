using API.Contracts;
using API.DataTransferObjects.Creates;
using API.DataTransferObjects;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Repositories;
using API.Utilities.Handlers;
using API.Utilities;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

/*
 * Education Controller adalah class untuk yang mengatur penerimaan request dan pengembalian response API.
 * Class ini terhubung dengan class Education Repository yang berfungsi untuk melakukan ORM.
 * Success dan Error Response di dalam controller ini di handle oleh ControllerBase dan Utility Class
 * terkait format Response API.
 */

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EducationController : ControllerBase
{
    private readonly EducationRepository _repository;

    public EducationController(EducationRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        // Get data collection from repository
        IEnumerable<Education> dataCollection = _repository.GetAll();

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
        IEnumerable<EducationDTO> data = dataCollection.Select(item => (EducationDTO)item);
        ResponseOKHandler<IEnumerable<EducationDTO>> response = new ResponseOKHandler<IEnumerable<EducationDTO>>(data);

        return Ok(response);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        // Check if data available
        Education? data = _repository.GetByGuid(guid);

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
        EducationDTO response = (EducationDTO)data;

        return Ok(response);
    }

    [HttpPost]
    public IActionResult Create(CreateEducationDTO educationDTO)
    {
        try
        {
            // Create data from request paylod
            Education? result = _repository.Create(educationDTO);

            // Return success response
            ResponseOKHandler<EducationDTO> response = new ResponseOKHandler<EducationDTO>((EducationDTO)result);

            return Ok(response);
        }
        catch (ExceptionHandler ex)
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
    public IActionResult Update(EducationDTO educationDTO)
    {
        try
        {
            // Check if data available
            Education? entity = _repository.GetByGuid(educationDTO.Guid);

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
            Education toUpdate = educationDTO;
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
        catch (ExceptionHandler ex)
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
            Education? data = _repository.GetByGuid(guid);

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
            ResponseOKHandler<string> response = new ResponseOKHandler<string>(Message.DataDeleted);

            return Ok(response);
        }
        catch (ExceptionHandler ex)
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
