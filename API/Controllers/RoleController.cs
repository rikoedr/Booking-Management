using API.Contracts;
using API.DataTransferObjects.Creates;
using API.DataTransferObjects;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IGeneralRepository<Role> _repository;

    public RoleController(IGeneralRepository<Role> repository)
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

        var data = result.Select(item => (RoleDTO)item);

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

        return Ok((RoleDTO)result);
    }

    [HttpPost]
    public IActionResult Create(CreateRoleDTO roleDTO)
    {
        var result = _repository.Create(roleDTO);

        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok((RoleDTO)result);
    }

    [HttpPut]
    public IActionResult Update(RoleDTO roleDTO)
    {
        var entity = _repository.GetByGuid(roleDTO.Guid);

        if (entity is null)
        {
            return NotFound("Id not found");
        }

        Role toUpdate = roleDTO;
        toUpdate.CreatedDate = entity.CreatedDate;

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
