using API.Contracts;
using API.DataTransferObjects;
using API.DataTransferObjects.Creates;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UniversityController : ControllerBase
{
    private readonly IGeneralRepository<University> _repository;

    public UniversityController(IGeneralRepository<University> repository)
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

        var data = result.Select(item => (UniversityDTO) item);

        return Ok(data);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _repository.GetByGuid(guid);

        if(result is null)
        {
            return NotFound("Id not found");
        }

        return Ok((UniversityDTO) result);
    }

    [HttpPost]
    public IActionResult Create(CreateUniversityDTO universityDTO)
    {
        var result = _repository.Create(universityDTO);

        if(result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok((UniversityDTO)result);
    }

    [HttpPut]
    public IActionResult Update(UniversityDTO universityDTO)
    {
        var entity = _repository.GetByGuid(universityDTO.Guid);

        if(entity is null)
        {
            return NotFound("Id not found");
        }

        University toUpdate = universityDTO;
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

        if(entity is null)
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
