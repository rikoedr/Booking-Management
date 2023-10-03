using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.DataTransferObjects;
using API.DataTransferObjects.Creates;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IGeneralRepository<Employee> _repository;

    public EmployeeController(IGeneralRepository<Employee> repository)
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

        var data = result.Select(item => (EmployeeDTO)item);

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

        return Ok((EmployeeDTO)result);
    }

    [HttpPost]
    public IActionResult Create(CreateEmployeeDTO employeeDTO)
    {
        var result = _repository.Create(employeeDTO);

        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok((EmployeeDTO)result);
    }

    [HttpPut]
    public IActionResult Update(EmployeeDTO employeeDTO)
    {
        var entity = _repository.GetByGuid(employeeDTO.Guid);

        if (entity is null)
        {
            return NotFound("Id not found");
        }

        Employee toUpdate = employeeDTO;
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
