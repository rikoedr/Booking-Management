using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.DataTransferObjects;
using API.DataTransferObjects.Creates;
using API.Utilities.Handlers;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IGeneralRepository<Account> _repository;

    public AccountController(IGeneralRepository<Account> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _repository.GetAll();

        if (!result.Any())
        {
            return NotFound("Data not Found");
        }

        var data = result.Select(item => (AccountDTO)item);

        return Ok(data);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _repository.GetByGuid(guid);

        if (result is null)
        {
            return NotFound("Id not found");
        }

        return Ok((AccountDTO)result);
    }

    [HttpPost]
    public IActionResult Create(CreateAccountDTO accountDTO)
    {
        var toCreate = accountDTO;
        toCreate.Password = HashingHandler.HashPassword(toCreate.Password);

        var result = _repository.Create(toCreate);

        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok((AccountDTO)result);
    }

    [HttpPut]
    public IActionResult Update(AccountDTO accountDTO)
    {
        var entity = _repository.GetByGuid(accountDTO.Guid);

        if (entity is null)
        {
            return NotFound("Id not found");
        }

        Account toUpdate = accountDTO;
        toUpdate.CreatedDate = entity.CreatedDate;
        toUpdate.Password = HashingHandler.HashPassword(toUpdate.Password);

        var result = _repository.Update(toUpdate);

        if (!result)
        {
            return BadRequest("Failed to update data");
        }

        return Ok("Data updated");
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var entity = _repository.GetByGuid(guid);

        if (entity is null)
        {
            return NotFound("Id not found");
        }

        var result = _repository.Delete(entity);

        if (!result)
        {
            return BadRequest("Failed to delete data");
        }

        return Ok("Data deleted");
    }
}
