using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.DataTransferObjects;
using API.DataTransferObjects.Creates;
using API.Utilities.Handlers;
using API.Utilities;
using System.Net;
using API.Repositories;

namespace API.Controllers;

/*
 * Account Controller adalah class untuk yang mengatur penerimaan request dan pengembalian response API.
 * Class ini terhubung dengan class Account Repository yang berfungsi untuk melakukan ORM.
 * Success dan Error Response di dalam controller ini di handle oleh ControllerBase dan Utility Class
 * terkait format Response API.
 */

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountRepository _repository;

    public AccountController(AccountRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        // Get data collection from repository
        IEnumerable<Account> dataCollection = _repository.GetAll();

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
        IEnumerable<AccountDTO> data = dataCollection.Select(item => (AccountDTO)item);
        ResponseOKHandler<IEnumerable<AccountDTO>> response = new ResponseOKHandler<IEnumerable<AccountDTO>>(data);

        return Ok(response);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        // Check if data available
        Account? data = _repository.GetByGuid(guid);

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
        AccountDTO response = (AccountDTO)data;

        return Ok(response);
    }

    [HttpPost]
    public IActionResult Create(CreateAccountDTO accountDTO)
    {
        try
        {
            // Create data from request paylod
            Account toCreate = accountDTO;
            toCreate.Password = HashingHandler.HashPassword(accountDTO.Password);

            // Return success response
            Account? result = _repository.Create(toCreate);
            ResponseOKHandler<AccountDTO> response = new ResponseOKHandler<AccountDTO>((AccountDTO)result);

            return Ok(response);
        }
        catch (ExceptionHandler ex)
        {
            ResponseErrorHandler response = new ResponseErrorHandler()
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
    public IActionResult Update(AccountDTO accountDTO)
    {
        try
        {
            // Check if data available
            Account? entity = _repository.GetByGuid(accountDTO.Guid);

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
            Account toUpdate = accountDTO;
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
            Account? data = _repository.GetByGuid(guid);

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
