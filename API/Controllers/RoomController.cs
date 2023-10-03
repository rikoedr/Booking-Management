using API.Contracts;
using API.DataTransferObjects.Creates;
using API.DataTransferObjects;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IGeneralRepository<Room> _repository;

    public RoomController(IGeneralRepository<Room> repository)
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

        var data = result.Select(item => (RoomDTO)item);

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

        return Ok((RoomDTO)result);
    }

    [HttpPost]
    public IActionResult Create(CreateRoomDTO roomDTO)
    {
        var result = _repository.Create(roomDTO);

        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok((RoomDTO)result);
    }

    [HttpPut]
    public IActionResult Update(RoomDTO roomDTO)
    {
        var entity = _repository.GetByGuid(roomDTO.Guid);

        if (entity is null)
        {
            return NotFound("Id not found");
        }

        Room toUpdate = roomDTO;
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
