using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.DataTransferObjects;
using API.DataTransferObjects.Creates;
using API.Utilities.Handlers;
using System.Net;
using API.Repositories;
using API.Utilities;

namespace API.Controllers;

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
        var result = _repository.GetAll();

        if (!result.Any())
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = Message.DataNotFound

            });
        }

        var data = result.Select(item => (EmployeeDTO)item);
        var response = new ResponseOKHandler<IEnumerable<EmployeeDTO>>(data);

        return Ok(response);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _repository.GetByGuid(guid);

        if (result is null)
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = Message.DataNotFound
            });
        }

        var response = (EmployeeDTO)result;

        return Ok(response);
    }

    [HttpPost]
    public IActionResult Create(CreateEmployeeDTO employeeDTO)
    {
        try
        {
            Employee toCreate = employeeDTO;
            toCreate.NIK = GenerateHandler.CreateNIK(_repository.GetLastNIK());

            var result = _repository.Create(toCreate);
            var response = new ResponseOKHandler<EmployeeDTO>((EmployeeDTO)result);

            return Ok(response);
        }
        catch(ExceptionHandler ex)
        {
            var response = new ResponseErrorHandler()
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
    public IActionResult Update(EmployeeDTO employeeDTO)
    {
        try
        {
            var entity = _repository.GetByGuid(employeeDTO.Guid);

            if(entity is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = Message.DataNotFound
                });
            }

            Employee toUpdate = employeeDTO;
            toUpdate.NIK = entity.NIK;
            toUpdate.CreatedDate = entity.CreatedDate;

            var result = _repository.Update(toUpdate);

            if (!result)
            {
                throw new Exception();
            }

            var response = new ResponseOKHandler<string>(Message.DataUpdated);

            return Ok(response);
        }
        catch(ExceptionHandler ex)
        {
            var response = new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = Message.FailedToUpdateData,
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
            var entity = _repository.GetByGuid(guid);
            if (entity is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = Message.DataNotFound
                });
            }

            var result = _repository.Delete(entity);

            if (!result)
            {
                throw new Exception();
            }

            var response = new ResponseOKHandler<string>(Message.DataDeleted);

            return Ok(response);
        }
        catch(ExceptionHandler ex)
        {
            var response = new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = Message.FailedToDeleteData,
                Error = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
