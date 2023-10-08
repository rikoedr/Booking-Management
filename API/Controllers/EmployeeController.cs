using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.DataTransferObjects;
using API.DataTransferObjects.Creates;
using API.Utilities.Handlers;
using System.Net;
using API.Repositories;
using API.Utilities.Responses;

namespace API.Controllers;

/*
 * Employee Controller adalah class untuk yang mengatur penerimaan request dan pengembalian response API.
 * Class ini terhubung dengan class Employee Repository yang berfungsi untuk melakukan ORM.
 * Success dan Error Response di dalam controller ini di handle oleh ControllerBase dan Utility Class
 * terkait format Response API.
 */

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeRepository _repository;

    public EmployeeController(EmployeeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        // Get data collection from repository
        IEnumerable<Employee> dataCollection = _repository.GetAll();

        // Handling empty collection
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
        IEnumerable<EmployeeDTO> data = dataCollection.Select(item => (EmployeeDTO)item);
        ResponseOKHandler<IEnumerable<EmployeeDTO>> response = new ResponseOKHandler<IEnumerable<EmployeeDTO>>(data);

        return Ok(response);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        // Check if data available
        Employee? data = _repository.GetByGuid(guid);

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

        // Return success response 
        EmployeeDTO response = (EmployeeDTO)data;

        return Ok(response);
    }

    [HttpPost]
    public IActionResult Create(CreateEmployeeDTO employeeDTO)
    {
        try
        {
            // Create data from request paylod
            Employee toCreate = employeeDTO;
            toCreate.NIK = GenerateHandler.NIK(_repository.GetLastNIK());

            // Return success response
            Employee? result = _repository.Create(toCreate);
            ResponseOKHandler<EmployeeDTO> response = new ResponseOKHandler<EmployeeDTO>((EmployeeDTO)result);

            return Ok(response);
        }
        catch(ExceptionHandler ex)
        {
            ResponseErrorHandler response = new ResponseErrorHandler()
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
    public IActionResult Update(EmployeeDTO employeeDTO)
    {
        try
        {
            // Check if data available
            Employee? data = _repository.GetByGuid(employeeDTO.Guid);

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

            // Update data
            Employee toUpdate = employeeDTO;
            toUpdate.NIK = data.NIK;
            toUpdate.CreatedDate = data.CreatedDate;

            bool result = _repository.Update(toUpdate);

            // Throw an exception if update failed
            if (!result)
            {
                throw new ExceptionHandler(Messages.ErrorOnUpdatingData);
            }

            // Return success response
            ResponseOKHandler<string> response = new ResponseOKHandler<string>(Messages.DataUpdated);

            return Ok(response);
        }
        catch(ExceptionHandler ex)
        {
            ResponseErrorHandler response = new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = Messages.FailedToUpdateData,
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
            Employee? entity = _repository.GetByGuid(guid);

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

            // Delete data
            bool result = _repository.Delete(entity);

            // Throw an exception if update failed
            if (!result)
            {
                throw new ExceptionHandler(Messages.ErrorOnDeletingData);
            }

            // Return success response
            ResponseOKHandler<string> response = new ResponseOKHandler<string>(Messages.DataDeleted);

            return Ok(response);
        }
        catch(ExceptionHandler ex)
        {

            ResponseErrorHandler response = new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = Messages.FailedToDeleteData,
                Error = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

   
}
